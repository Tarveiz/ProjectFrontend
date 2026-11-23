namespace FrontTestTask.Service.Services.Interfaces;

/// <summary>
/// Сценарии входа, регистрации и выхода.
/// </summary>
public interface IAuthService
{
    public Task LoginAsync(string email, string password, CancellationToken cancellationToken);

    public Task RegisterAsync(string email, string name, string password, CancellationToken cancellationToken);

    public Task LogoutAsync(CancellationToken cancellationToken);
}
