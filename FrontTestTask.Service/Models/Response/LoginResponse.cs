using FrontTestTask.Service.Models.Dto;

namespace FrontTestTask.Service.Models.Response;

/// <summary>
/// Ответ на авторизацию.
/// </summary>
public sealed record LoginResponse(string AccessToken, string RefreshToken, string TokenType, int ExpiresIn, UserDto User);
