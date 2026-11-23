namespace FrontTestTask.Service.Models.Request;

/// <summary>
/// Тело запроса выхода из аккаунта.
/// </summary>
public sealed record LogoutRequest(string RefreshToken);
