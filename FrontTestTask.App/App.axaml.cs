using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using FrontTestTask.App.Infrastructure;
using FrontTestTask.App.Views;
using FrontTestTask.Common.DependencyInjection;
using FrontTestTask.Common.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace FrontTestTask.App;

public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var services = new ServiceCollection().AddCommon().AddApp().BuildServiceProvider();

            desktop.MainWindow = new MainWindow { DataContext = services.GetRequiredService<MainViewModel>() };
            desktop.Exit += (_, _) => services.Dispose();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
