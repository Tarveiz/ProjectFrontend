using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using FrontTestTask.App.Extensions;
using FrontTestTask.App.Infrastructure;
using FrontTestTask.App.Views;
using FrontTestTask.Common.Extensions;
using FrontTestTask.Common.ViewModels;
using FrontTestTask.Service.Api;
using FrontTestTask.Service.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace FrontTestTask.App;

public partial class App : Application
{
    private static readonly Uri ApiBaseAddress = new("https://fakeapifordevs.vercel.app/");

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var services = new ServiceCollection().AddServices(new ApiOptions { BaseAddress = ApiBaseAddress }).AddCommon().AddApp().BuildServiceProvider();

            DataTemplates.Add(services.GetRequiredService<ViewLocator>());

            desktop.MainWindow = new MainWindow { DataContext = services.GetRequiredService<MainViewModel>() };
            desktop.Exit += (_, _) => services.Dispose();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
