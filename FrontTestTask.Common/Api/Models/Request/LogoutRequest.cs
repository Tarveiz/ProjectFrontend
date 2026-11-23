namespace FrontTestTask.Common.Api.Models.Request;

/// <summary>
/// Тело запроса выхода из аккаунта.
/// </summary>
public sealed record LogoutRequest(string RefreshToken);
