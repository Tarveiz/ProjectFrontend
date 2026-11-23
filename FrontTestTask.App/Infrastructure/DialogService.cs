using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using FrontTestTask.App.Views;
using FrontTestTask.Common.ViewModels;
using FrontTestTask.Common.ViewModels.Interfaces;

namespace FrontTestTask.App.Infrastructure;

public sealed class DialogService(Func<RegisterViewModel> createRegisterViewModel) : IDialogService
{
    public Task ShowRegistrationAsync()
    {
        var viewModel = createRegisterViewModel();
        var window = new RegisterWindow { DataContext = viewModel };

        viewModel.CloseRequested += (_, result) => window.Close(result);
        window.Closed += (_, _) => viewModel.RegisterCommand.Cancel();

        return window.ShowDialog(GetOwner());
    }

    private static Window GetOwner()
        => Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: { } mainWindow } ? mainWindow : throw new InvalidOperationException("Main window is not available.");
}
