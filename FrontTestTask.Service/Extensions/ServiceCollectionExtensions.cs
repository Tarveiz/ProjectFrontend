using System.Net;
using FrontTestTask.Service.Api;
using FrontTestTask.Service.Api.Interfaces;
using FrontTestTask.Service.Services;
using FrontTestTask.Service.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FrontTestTask.Service.Extensions;

/// <summary>
/// Регистрация API-клиента и сервисов.
/// </summary>
public static class ServiceCollectionExtensions
{
    private static readonly TimeSpan PooledConnectionLifetime = TimeSpan.FromMinutes(5);

    public static IServiceCollection AddServices(this IServiceCollection services, ApiOptions apiOptions)
    {
        services.AddSingleton(apiOptions);

        services.AddSingleton<ISessionService, SessionService>();
        services.AddSingleton<IAccessTokenProvider>(provider => provider.GetRequiredService<ISessionService>());
        services.AddSingleton<IAuthService, AuthService>();

        services.AddTransient<AuthHeaderHandler>();
        services.AddHttpClient<IAuthApiClient, AuthApiClient>(client => ConfigureClient(client, apiOptions))
            .ConfigurePrimaryHttpMessageHandler(CreatePrimaryHandler)
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan)
            .AddHttpMessageHandler<AuthHeaderHandler>();

        return services;
    }

    private static void ConfigureClient(HttpClient client, ApiOptions apiOptions)
    {
        client.BaseAddress = apiOptions.BaseAddress;
        client.Timeout = apiOptions.Timeout;
    }

    private static HttpMessageHandler CreatePrimaryHandler()
        => new SocketsHttpHandler { PooledConnectionLifetime = PooledConnectionLifetime, AutomaticDecompression = DecompressionMethods.All };
}
