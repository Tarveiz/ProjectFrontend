namespace FrontTestTask.Service.Models.Dto;

/// <summary>
/// Пользователь в ответах API. Avatar приходит только из GET /api/auth/me.
/// </summary>
public sealed record UserDto(string Id, string Email, string Name, string Role, string? Avatar);
