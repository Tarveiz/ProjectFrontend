using CommunityToolkit.Mvvm.ComponentModel;
using FrontTestTask.Service.Api;
using Microsoft.Extensions.Logging;

namespace FrontTestTask.Common.ViewModels;

/// <summary>
/// Базовый класс форм, выполняющих запросы к серверу. Единая точка обработки ошибок для UI.
/// </summary>
public abstract partial class FormViewModelBase(ILogger logger) : ViewModelBase
{
    private const string UnexpectedErrorMessage = "Что-то пошло не так, попробуйте ещё раз";

    [ObservableProperty]
    private string? _errorMessage;

    protected async Task<bool> ExecuteAsync(Func<Task> operation)
    {
        ErrorMessage = null;
        try
        {
            await operation();
            return true;
        }
        catch (ApiException ex) { ErrorMessage = ex.Message; }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error in {ViewModel}", GetType().Name);
            ErrorMessage = UnexpectedErrorMessage;
        }

        return false;
    }
}
