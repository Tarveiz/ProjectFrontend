namespace FrontTestTask.Service.Api;

/// <summary>
/// Параметры подключения к API.
/// </summary>
public sealed class ApiOptions
{
    public required Uri BaseAddress { get; init; }

    public TimeSpan Timeout { get; init; } = TimeSpan.FromSeconds(15);
}
