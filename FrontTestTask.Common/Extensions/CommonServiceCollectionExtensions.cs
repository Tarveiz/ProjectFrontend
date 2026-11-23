using FrontTestTask.Common.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace FrontTestTask.Common.Extensions;

/// <summary>
/// Регистрация ViewModel.
/// </summary>
public static class CommonServiceCollectionExtensions
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();

        return services;
    }
}
