using FrontTestTask.Service.Models.Dto;

namespace FrontTestTask.Service.Models.Response;

/// <summary>
/// Ответ на обновление токенов.
/// </summary>
public sealed record RefreshTokenResponse(string AccessToken, string RefreshToken, string TokenType, int ExpiresIn, UserDto User);
