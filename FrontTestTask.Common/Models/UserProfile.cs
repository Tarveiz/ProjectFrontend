namespace FrontTestTask.Common.Models;

/// <summary>
/// Пользователь для отображения в UI.
/// </summary>
public sealed record UserProfile(string Id, string Email, string Name, string Role, string? AvatarUrl);
