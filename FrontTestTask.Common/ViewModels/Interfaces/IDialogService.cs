namespace FrontTestTask.Common.ViewModels.Interfaces;

/// <summary>
/// Открытие диалоговых окон из ViewModel без ссылок на View.
/// </summary>
public interface IDialogService
{
    public Task ShowRegistrationAsync();
}
