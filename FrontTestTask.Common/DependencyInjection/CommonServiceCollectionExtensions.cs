using FrontTestTask.Common.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace FrontTestTask.Common.DependencyInjection;

/// <summary>
/// Регистрация сервисов и ViewModel.
/// </summary>
public static class CommonServiceCollectionExtensions
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();

        return services;
    }
}
