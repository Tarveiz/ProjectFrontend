using System.Net;
using FrontTestTask.Common.Api;
using FrontTestTask.Common.Api.Interfaces;
using FrontTestTask.Common.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace FrontTestTask.Common.Extensions;

/// <summary>
/// Регистрация сервисов и ViewModel.
/// </summary>
public static class CommonServiceCollectionExtensions
{
    private static readonly TimeSpan PooledConnectionLifetime = TimeSpan.FromMinutes(5);

    public static IServiceCollection AddCommon(this IServiceCollection services, ApiOptions apiOptions)
    {
        services.AddSingleton(apiOptions);

        services.AddTransient<AuthHeaderHandler>();
        services.AddHttpClient<IAuthApiClient, AuthApiClient>(client => ConfigureClient(client, apiOptions))
            .ConfigurePrimaryHttpMessageHandler(CreatePrimaryHandler)
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan)
            .AddHttpMessageHandler<AuthHeaderHandler>();

        services.AddSingleton<MainViewModel>();

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
