using FrontTestTask.Common.Models;

namespace FrontTestTask.Common.Messages;

/// <summary>
/// Изменение сессии при входе и выходе.
/// </summary>
public sealed record SessionChangedMessage(UserProfile? User);
