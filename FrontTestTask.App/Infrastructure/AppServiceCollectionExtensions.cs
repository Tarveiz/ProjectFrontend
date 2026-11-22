using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FrontTestTask.App.Infrastructure;

/// <summary>
/// Регистрация сервисов уровня UI.
/// </summary>
public static class AppServiceCollectionExtensions
{
    public static IServiceCollection AddApp(this IServiceCollection services)
    {
        services.AddLogging(builder => builder.AddDebug());

        return services;
    }
}
