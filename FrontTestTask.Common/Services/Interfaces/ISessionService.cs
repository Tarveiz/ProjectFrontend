using FrontTestTask.Common.Api.Interfaces;
using FrontTestTask.Common.Models;

namespace FrontTestTask.Common.Services.Interfaces;

/// <summary>
/// Состояние сессии. Токены хранятся только в памяти.
/// </summary>
public interface ISessionService : IAccessTokenProvider
{
    public string? RefreshToken { get; }

    public UserProfile? CurrentUser { get; }

    public bool IsAuthenticated { get; }

    public void SetTokens(string accessToken, string refreshToken);

    public void SetUser(UserProfile user);

    public void Clear();
}
