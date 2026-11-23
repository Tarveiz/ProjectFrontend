using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FrontTestTask.Common.ViewModels.Interfaces;
using FrontTestTask.Service.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace FrontTestTask.Common.ViewModels;

public sealed partial class AuthViewModel : FormViewModelBase
{
    private readonly IAuthService _authService;
    private readonly ISessionService _session;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    [Required(ErrorMessage = "Введите email")]
    [RegularExpression(ValidationPatterns.Email, ErrorMessage = "Некорректный email")]
    private string _email = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    [Required(ErrorMessage = "Введите пароль")]
    [MinLength(6, ErrorMessage = "Не менее 6 символов")]
    private string _password = string.Empty;

    [ObservableProperty]
    private bool _isAuthenticated;

    [ObservableProperty]
    private string? _userName;

    public AuthViewModel(IAuthService authService, ISessionService session, IDialogService dialogService, ILogger<AuthViewModel> logger) : base(logger)
    {
        _authService = authService;
        _session = session;
        _dialogService = dialogService;

        _session.SessionChanged += OnSessionChanged;
        ErrorsChanged += (_, _) => LoginCommand.NotifyCanExecuteChanged();
    }

    private bool CanLogin() => !HasErrors && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrEmpty(Password);

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task LoginAsync(CancellationToken cancellationToken)
    {
        ValidateAllProperties();
        if (HasErrors)
            return;

        await ExecuteAsync(() => _authService.LoginAsync(Email.Trim(), Password, cancellationToken));
    }

    [RelayCommand]
    private Task LogoutAsync(CancellationToken cancellationToken) => ExecuteAsync(() => _authService.LogoutAsync(cancellationToken));

    [RelayCommand]
    private Task OpenRegistrationAsync()
    {
        ErrorMessage = null;
        return _dialogService.ShowRegistrationAsync();
    }

    private void OnSessionChanged(object? sender, EventArgs e)
    {
        IsAuthenticated = _session.IsAuthenticated;
        UserName = _session.CurrentUser?.Name;

        Email = string.Empty;
        Password = string.Empty;
        ClearErrors();
    }
}
