namespace FrontTestTask.Service.Models.Response;

/// <summary>
/// Тело ошибки API.
/// </summary>
public sealed record ApiErrorResponse(string? Error, string? Message);
