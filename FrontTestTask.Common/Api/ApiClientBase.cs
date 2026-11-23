using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using FrontTestTask.Common.Api.Models.Response;
using Microsoft.Extensions.Logging;

namespace FrontTestTask.Common.Api;

/// <summary>
/// Базовый клиент API: отправка запросов, чтение JSON-ответов и единое преобразование ошибок в <see cref="ApiException"/>.
/// </summary>
public abstract class ApiClientBase(HttpClient httpClient, ILogger logger)
{
    protected Task<TResponse> GetAsync<TResponse>(string path, JsonTypeInfo<TResponse> responseInfo, CancellationToken cancellationToken)
        => SendAsync(new HttpRequestMessage(HttpMethod.Get, path), (content, token) => ReadJsonAsync(content, responseInfo, token), cancellationToken);

    protected Task<TResponse> PostAsync<TRequest, TResponse>(string path, TRequest body, JsonTypeInfo<TRequest> requestInfo, JsonTypeInfo<TResponse> responseInfo, CancellationToken cancellationToken)
        => SendAsync(CreatePostRequest(path, body, requestInfo), (content, token) => ReadJsonAsync(content, responseInfo, token), cancellationToken);

    protected Task PostAsync<TRequest>(string path, TRequest body, JsonTypeInfo<TRequest> requestInfo, CancellationToken cancellationToken)
        => SendAsync(CreatePostRequest(path, body, requestInfo), static (_, _) => Task.FromResult(true), cancellationToken);

    private async Task<TResponse> SendAsync<TResponse>(HttpRequestMessage request, Func<HttpContent, CancellationToken, Task<TResponse>> readContent, CancellationToken cancellationToken)
    {
        using (request)
        {
            try
            {
                using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                    throw await CreateErrorAsync(request, response, cancellationToken).ConfigureAwait(false);

                return await readContent(response.Content, cancellationToken).ConfigureAwait(false);
            }
            catch (ApiException) { throw; }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { throw; }
            catch (OperationCanceledException ex) { throw CreateTransportError(request, ApiErrorMessages.Timeout, ex); }
            catch (HttpRequestException ex) { throw CreateTransportError(request, ApiErrorMessages.Network, ex); }
            catch (JsonException ex) { throw CreateTransportError(request, ApiErrorMessages.InvalidResponse, ex); }
        }
    }

    private static HttpRequestMessage CreatePostRequest<TRequest>(string path, TRequest body, JsonTypeInfo<TRequest> requestInfo)
        => new(HttpMethod.Post, path) { Content = JsonContent.Create(body, requestInfo) };

    private static async Task<TResponse> ReadJsonAsync<TResponse>(HttpContent content, JsonTypeInfo<TResponse> responseInfo, CancellationToken cancellationToken)
        => await content.ReadFromJsonAsync(responseInfo, cancellationToken).ConfigureAwait(false) ?? throw new JsonException("Response body is empty.");

    private async Task<ApiException> CreateErrorAsync(HttpRequestMessage request, HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var error = await TryReadErrorAsync(response.Content, cancellationToken).ConfigureAwait(false);
        var message = FirstNotEmpty(error?.Message, error?.Error) ?? ApiErrorMessages.ForStatusCode(response.StatusCode);
        var level = response.StatusCode >= HttpStatusCode.InternalServerError ? LogLevel.Error : LogLevel.Warning;

        logger.Log(level, "Request {Method} {Uri} failed with status {StatusCode}: {Message}", request.Method, request.RequestUri, (int)response.StatusCode, message);

        return new ApiException(response.StatusCode, message);
    }

    private ApiException CreateTransportError(HttpRequestMessage request, string message, Exception exception)
    {
        logger.LogError(exception, "Request {Method} {Uri} failed: {Message}", request.Method, request.RequestUri, message);

        return new ApiException(null, message, exception);
    }

    private static async Task<ApiErrorResponse?> TryReadErrorAsync(HttpContent content, CancellationToken cancellationToken)
    {
        try
        {
            return await content.ReadFromJsonAsync(ApiJsonContext.Default.ApiErrorResponse, cancellationToken).ConfigureAwait(false);
        }
        catch (JsonException) { return null; }
    }

    private static string? FirstNotEmpty(params ReadOnlySpan<string?> values)
    {
        foreach (var value in values)
        {
            if (!string.IsNullOrWhiteSpace(value))
                return value;
        }

        return null;
    }
}
