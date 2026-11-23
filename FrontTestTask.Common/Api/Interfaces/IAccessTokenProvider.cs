namespace FrontTestTask.Common.Api.Interfaces;

/// <summary>
/// Источник текущего access-токена для подстановки в запросы.
/// </summary>
public interface IAccessTokenProvider
{
    string? AccessToken { get; }
}
