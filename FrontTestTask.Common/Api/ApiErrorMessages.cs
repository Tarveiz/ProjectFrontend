using System.Net;

namespace FrontTestTask.Common.Api;

/// <summary>
/// Тексты ошибок для пользователя, когда сервер не прислал собственного описания.
/// </summary>
internal static class ApiErrorMessages
{
    public const string Network = "Нет соединения с сервером";
    public const string Timeout = "Сервер не отвечает";
    public const string InvalidResponse = "Некорректный ответ сервера";

    public static string ForStatusCode(HttpStatusCode statusCode) => statusCode switch
    {
        HttpStatusCode.BadRequest => "Проверьте введённые данные",
        HttpStatusCode.Unauthorized => "Требуется авторизация",
        HttpStatusCode.TooManyRequests => "Слишком много запросов, попробуйте позже",
        >= HttpStatusCode.InternalServerError => "Ошибка сервера, попробуйте позже",
        _ => "Не удалось выполнить запрос"
    };
}
