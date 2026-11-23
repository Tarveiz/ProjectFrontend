using FrontTestTask.Service.Models.Dto;
using FrontTestTask.Service.Models.Request;
using FrontTestTask.Service.Models.Response;

namespace FrontTestTask.Service.Api.Interfaces;

/// <summary>
/// Методы авторизации API. При ошибке выбрасывают <see cref="ApiException"/>.
/// </summary>
public interface IAuthApiClient
{
    public Task<UserDto> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);

    public Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken);

    public Task<RefreshTokenResponse> RefreshAsync(string refreshToken, CancellationToken cancellationToken);

    public Task<UserDto> GetCurrentUserAsync(CancellationToken cancellationToken);

    public Task LogoutAsync(string refreshToken, CancellationToken cancellationToken);
}
