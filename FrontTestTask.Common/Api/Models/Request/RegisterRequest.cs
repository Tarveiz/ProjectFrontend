namespace FrontTestTask.Common.Api.Models.Request;

/// <summary>
/// Тело запроса регистрации.
/// </summary>
public sealed record RegisterRequest(string Email, string Password, string Name);
