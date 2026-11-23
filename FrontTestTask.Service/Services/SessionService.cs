using FrontTestTask.Service.Models.Dto;
using FrontTestTask.Service.Services.Interfaces;

namespace FrontTestTask.Service.Services;

public sealed class SessionService : ISessionService
{
    private volatile SessionTokens? _tokens;

    public event EventHandler? SessionChanged;

    public string? AccessToken => _tokens?.AccessToken;

    public string? RefreshToken => _tokens?.RefreshToken;

    public UserDto? CurrentUser { get; private set; }

    public bool IsAuthenticated => CurrentUser is not null;

    public void SetTokens(string accessToken, string refreshToken) => _tokens = new SessionTokens(accessToken, refreshToken);

    public void SetUser(UserDto user)
    {
        CurrentUser = user;
        SessionChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Clear()
    {
        var wasAuthenticated = IsAuthenticated;

        _tokens = null;
        CurrentUser = null;

        if (wasAuthenticated)
            SessionChanged?.Invoke(this, EventArgs.Empty);
    }

    private sealed record SessionTokens(string AccessToken, string RefreshToken);
}
