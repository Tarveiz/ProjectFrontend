namespace FrontTestTask.Common.Api.Models.Request;

/// <summary>
/// Тело запроса на авторизацию.
/// </summary>
public sealed record LoginRequest(string Email, string Password);
