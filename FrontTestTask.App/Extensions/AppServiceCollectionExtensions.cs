using System.Net;
using Avalonia.Controls;
using FrontTestTask.App.Infrastructure;
using FrontTestTask.App.Views.Sections;
using FrontTestTask.Common.ViewModels;
using FrontTestTask.Common.ViewModels.Interfaces;
using FrontTestTask.Common.ViewModels.Sections;
using FrontTestTask.Service.Api;
using FrontTestTask.Service.Api.Interfaces;
using FrontTestTask.Service.Services;
using FrontTestTask.Service.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FrontTestTask.App.Extensions;

/// <summary>
/// Регистрация зависимостей всех слоёв приложения.
/// </summary>
public static class AppServiceCollectionExtensions
{
    private static readonly TimeSpan PooledConnectionLifetime = TimeSpan.FromMinutes(5);

    public static IServiceCollection AddServices(this IServiceCollection services, ApiOptions apiOptions)
    {
        services.AddLogging(builder => builder.AddDebug());

        services.AddSingleton<ISessionService, SessionService>();
        services.AddSingleton<IAccessTokenProvider>(provider => provider.GetRequiredService<ISessionService>());
        services.AddSingleton<IAuthService, AuthService>();

        services.AddTransient<AuthHeaderHandler>();
        services.AddHttpClient<IAuthApiClient, AuthApiClient>(client => ConfigureClient(client, apiOptions))
            .ConfigurePrimaryHttpMessageHandler(CreatePrimaryHandler)
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan)
            .AddHttpMessageHandler<AuthHeaderHandler>();

        services.AddHttpClient<IImageLoader, ImageLoader>(client => client.Timeout = apiOptions.Timeout)
            .ConfigurePrimaryHttpMessageHandler(CreatePrimaryHandler)
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

        return services;
    }

    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<AuthViewModel>();
        services.AddTransient<RegisterViewModel>();
        services.AddSingleton<Func<RegisterViewModel>>(provider => () => provider.GetRequiredService<RegisterViewModel>());

        return services;
    }

    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddSingleton<ViewLocator>();
        services.AddSingleton<IDialogService, DialogService>();

        services.AddSection<ProfileSectionViewModel, ProfileSectionView>();
        services.AddSection<AboutSectionViewModel, AboutSectionView>();

        return services;
    }

    private static IServiceCollection AddSection<TViewModel, TView>(this IServiceCollection services) where TViewModel : class, ISectionViewModel where TView : Control, new()
    {
        services.AddSingleton<ISectionViewModel, TViewModel>();
        services.AddSingleton(new ViewMapping(typeof(TViewModel), static () => new TView()));

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
