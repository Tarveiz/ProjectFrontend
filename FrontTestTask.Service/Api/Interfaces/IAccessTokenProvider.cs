namespace FrontTestTask.Service.Api.Interfaces;

/// <summary>
/// Источник текущего access-токена для подстановки в запросы.
/// </summary>
public interface IAccessTokenProvider
{
    public string? AccessToken { get; }
}
