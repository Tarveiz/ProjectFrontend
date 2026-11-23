using FrontTestTask.Common.Api.Models.Dto;
using FrontTestTask.Common.Api.Models.Request;
using FrontTestTask.Common.Api.Models.Response;

namespace FrontTestTask.Common.Api.Interfaces;

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
