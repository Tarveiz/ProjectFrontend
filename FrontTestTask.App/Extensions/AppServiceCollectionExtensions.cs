using Avalonia.Controls;
using FrontTestTask.App.Infrastructure;
using FrontTestTask.App.Views.Sections;
using FrontTestTask.Common.ViewModels.Interfaces;
using FrontTestTask.Common.ViewModels.Sections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FrontTestTask.App.Extensions;

/// <summary>
/// Регистрация сервисов уровня UI.
/// </summary>
public static class AppServiceCollectionExtensions
{
    public static IServiceCollection AddApp(this IServiceCollection services)
    {
        services.AddLogging(builder => builder.AddDebug());
        services.AddSingleton<ViewLocator>();

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
}
