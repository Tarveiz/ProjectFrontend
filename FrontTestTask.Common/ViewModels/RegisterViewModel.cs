using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FrontTestTask.Service.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace FrontTestTask.Common.ViewModels;

public sealed partial class RegisterViewModel : FormViewModelBase
{
    private readonly IAuthService _authService;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    [Required(ErrorMessage = "Введите email")]
    [RegularExpression(ValidationPatterns.Email, ErrorMessage = "Некорректный email")]
    private string _email = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    [Required(ErrorMessage = "Введите имя")]
    [MaxLength(100, ErrorMessage = "Не более 100 символов")]
    private string _name = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    [Required(ErrorMessage = "Введите пароль")]
    [MinLength(6, ErrorMessage = "Не менее 6 символов")]
    private string _password = string.Empty;

    public RegisterViewModel(IAuthService authService, ILogger<RegisterViewModel> logger) : base(logger)
    {
        _authService = authService;

        ErrorsChanged += (_, _) => RegisterCommand.NotifyCanExecuteChanged();
    }

    /// <summary>
    /// Запрос на закрытие окна.
    /// </summary>
    public event EventHandler<bool>? CloseRequested;

    private bool CanRegister() => !HasErrors && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrEmpty(Password);

    [RelayCommand(CanExecute = nameof(CanRegister))]
    private async Task RegisterAsync(CancellationToken cancellationToken)
    {
        ValidateAllProperties();
        if (HasErrors)
            return;

        if (await ExecuteAsync(() => _authService.RegisterAsync(Email.Trim(), Name.Trim(), Password, cancellationToken)))
            CloseRequested?.Invoke(this, true);
    }

    [RelayCommand]
    private void Cancel() => CloseRequested?.Invoke(this, false);
}
