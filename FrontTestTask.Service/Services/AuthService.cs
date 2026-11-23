using FrontTestTask.Service.Api;
using FrontTestTask.Service.Api.Interfaces;
using FrontTestTask.Service.Models.Dto;
using FrontTestTask.Service.Models.Request;
using FrontTestTask.Service.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace FrontTestTask.Service.Services;

public sealed class AuthService(IAuthApiClient authApiClient, ISessionService session, ILogger<AuthService> logger) : IAuthService
{
    public async Task LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        try
        {
            var response = await authApiClient.LoginAsync(new LoginRequest(email, password), cancellationToken);
            session.SetTokens(response.AccessToken, response.RefreshToken);

            var user = await GetCurrentUserAsync(response.User, cancellationToken);
            session.SetUser(user);

            logger.LogInformation("User {UserId} signed in", user.Id);
        }
        catch (Exception) { session.Clear(); throw; }
    }

    public async Task RegisterAsync(string email, string name, string password, CancellationToken cancellationToken)
    {
        var user = await authApiClient.RegisterAsync(new RegisterRequest(email, password, name), cancellationToken);
        logger.LogInformation("User {UserId} registered", user.Id);

        await LoginAsync(email, password, cancellationToken);
    }

    public async Task LogoutAsync(CancellationToken cancellationToken)
    {
        var refreshToken = session.RefreshToken;
        try
        {
            if (refreshToken is not null)
                await authApiClient.LogoutAsync(refreshToken, cancellationToken);
        }
        catch (ApiException) { }
        finally
        {
            session.Clear();
        }

        logger.LogInformation("User signed out");
    }

    private async Task<UserDto> GetCurrentUserAsync(UserDto fallback, CancellationToken cancellationToken)
    {
        try
        {
            return await authApiClient.GetCurrentUserAsync(cancellationToken);
        }
        catch (ApiException) { return fallback; }
    }
}
