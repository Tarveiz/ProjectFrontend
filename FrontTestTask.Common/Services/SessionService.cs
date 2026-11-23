using CommunityToolkit.Mvvm.Messaging;
using FrontTestTask.Common.Messages;
using FrontTestTask.Common.Models;
using FrontTestTask.Common.Services.Interfaces;

namespace FrontTestTask.Common.Services;

public sealed class SessionService(IMessenger messenger) : ISessionService
{
    private volatile SessionTokens? _tokens;

    public string? AccessToken => _tokens?.AccessToken;

    public string? RefreshToken => _tokens?.RefreshToken;

    public UserProfile? CurrentUser { get; private set; }

    public bool IsAuthenticated => CurrentUser is not null;

    public void SetTokens(string accessToken, string refreshToken) => _tokens = new SessionTokens(accessToken, refreshToken);

    public void SetUser(UserProfile user)
    {
        CurrentUser = user;
        messenger.Send(new SessionChangedMessage(user));
    }

    public void Clear()
    {
        var wasAuthenticated = IsAuthenticated;

        _tokens = null;
        CurrentUser = null;

        if (wasAuthenticated)
            messenger.Send(new SessionChangedMessage(null));
    }

    private sealed record SessionTokens(string AccessToken, string RefreshToken);
}
