using System.Net;

namespace FrontTestTask.Common.Api;

/// <summary>
/// Ошибка обращения к API.
/// StatusCode равен null, если ответ от сервера не получен (сеть, таймаут).
/// </summary>
public sealed class ApiException(HttpStatusCode? statusCode, string message, Exception? innerException = null) : Exception(message, innerException)
{
    public HttpStatusCode? StatusCode { get; } = statusCode;

    public bool IsUnauthorized => StatusCode == HttpStatusCode.Unauthorized;
}
