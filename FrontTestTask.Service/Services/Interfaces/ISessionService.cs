using FrontTestTask.Service.Api.Interfaces;
using FrontTestTask.Service.Models.Dto;

namespace FrontTestTask.Service.Services.Interfaces;

/// <summary>
/// Состояние сессии. Токены хранятся только в памяти.
/// </summary>
public interface ISessionService : IAccessTokenProvider
{
    public event EventHandler? SessionChanged;

    public string? RefreshToken { get; }

    public UserDto? CurrentUser { get; }

    public bool IsAuthenticated { get; }

    public void SetTokens(string accessToken, string refreshToken);

    public void SetUser(UserDto user);

    public void Clear();
}
