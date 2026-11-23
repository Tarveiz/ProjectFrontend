using FrontTestTask.Common.Api.Models.Dto;
using FrontTestTask.Common.Api.Models.Request;
using FrontTestTask.Common.Api.Models.Response;

namespace FrontTestTask.Common.Api.Interfaces;

/// <summary>
/// Методы авторизации API. При ошибке выбрасывают <see cref="ApiException"/>.
/// </summary>
public interface IAuthApiClient
{
    Task<UserDto> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);

    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken);

    Task<RefreshTokenResponse> RefreshAsync(string refreshToken, CancellationToken cancellationToken);

    Task<UserDto> GetCurrentUserAsync(CancellationToken cancellationToken);

    Task LogoutAsync(string refreshToken, CancellationToken cancellationToken);
}
