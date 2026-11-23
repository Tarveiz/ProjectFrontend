using FrontTestTask.Common.Api.Models.Dto;

namespace FrontTestTask.Common.Api.Models.Response;

/// <summary>
/// Ответ на обновление токенов.
/// </summary>
public sealed record RefreshTokenResponse(string AccessToken, string RefreshToken, string TokenType, int ExpiresIn, UserDto User);
