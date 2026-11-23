using FrontTestTask.Common.Api.Models.Dto;

namespace FrontTestTask.Common.Api.Models.Response;

/// <summary>
/// Ответ на авторизацию.
/// </summary>
public sealed record LoginResponse(string AccessToken, string RefreshToken, string TokenType, int ExpiresIn, UserDto User);
