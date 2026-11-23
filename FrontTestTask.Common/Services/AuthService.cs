using FrontTestTask.Common.Api;
using FrontTestTask.Common.Api.Interfaces;
using FrontTestTask.Common.Api.Models.Dto;
using FrontTestTask.Common.Api.Models.Request;
using FrontTestTask.Common.Extensions;
using FrontTestTask.Common.Models;
using FrontTestTask.Common.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace FrontTestTask.Common.Services;

public sealed class AuthService(IAuthApiClient authApiClient, ISessionService session, ILogger<AuthService> logger) : IAuthService
{
    public async Task LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        var response = await authApiClient.LoginAsync(new LoginRequest(email, password), cancellationToken);
        session.SetTokens(response.AccessToken, response.RefreshToken);

        UserProfile user;
        try
        {
            user = await GetProfileAsync(response.User, cancellationToken);
        }
        catch (OperationCanceledException) { session.Clear(); throw; }

        session.SetUser(user);
        logger.LogInformation("User {UserId} signed in", user.Id);
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

    private async Task<UserProfile> GetProfileAsync(UserDto fallback, CancellationToken cancellationToken)
    {
        try
        {
            return (await authApiClient.GetCurrentUserAsync(cancellationToken)).ToProfile();
        }
        catch (ApiException) { return fallback.ToProfile(); }
    }
}
