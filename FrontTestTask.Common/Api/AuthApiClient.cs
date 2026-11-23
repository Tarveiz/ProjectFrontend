using FrontTestTask.Common.Api.Interfaces;
using FrontTestTask.Common.Api.Models.Dto;
using FrontTestTask.Common.Api.Models.Request;
using FrontTestTask.Common.Api.Models.Response;
using Microsoft.Extensions.Logging;

namespace FrontTestTask.Common.Api;

public sealed class AuthApiClient(HttpClient httpClient, ILogger<AuthApiClient> logger) : ApiClientBase(httpClient, logger), IAuthApiClient
{
    private const string RegisterPath = "api/auth/register";
    private const string LoginPath = "api/auth/login";
    private const string RefreshPath = "api/auth/refresh";
    private const string CurrentUserPath = "api/auth/me";
    private const string LogoutPath = "api/auth/logout";

    public Task<UserDto> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        => PostAsync(RegisterPath, request, ApiJsonContext.Default.RegisterRequest, ApiJsonContext.Default.UserDto, cancellationToken);

    public Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        => PostAsync(LoginPath, request, ApiJsonContext.Default.LoginRequest, ApiJsonContext.Default.LoginResponse, cancellationToken);

    public Task<RefreshTokenResponse> RefreshAsync(string refreshToken, CancellationToken cancellationToken)
        => PostAsync(RefreshPath, new RefreshTokenRequest(refreshToken), ApiJsonContext.Default.RefreshTokenRequest, ApiJsonContext.Default.RefreshTokenResponse, cancellationToken);

    public Task<UserDto> GetCurrentUserAsync(CancellationToken cancellationToken)
        => GetAsync(CurrentUserPath, ApiJsonContext.Default.UserDto, cancellationToken);

    public Task LogoutAsync(string refreshToken, CancellationToken cancellationToken)
        => PostAsync(LogoutPath, new LogoutRequest(refreshToken), ApiJsonContext.Default.LogoutRequest, cancellationToken);
}
