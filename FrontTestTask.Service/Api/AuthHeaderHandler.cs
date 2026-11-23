using System.Net.Http.Headers;
using FrontTestTask.Service.Api.Interfaces;

namespace FrontTestTask.Service.Api;

/// <summary>
/// Добавляет заголовок Authorization: Bearer, если пользователь авторизован.
/// </summary>
public sealed class AuthHeaderHandler(IAccessTokenProvider tokenProvider) : DelegatingHandler
{
    private const string Scheme = "Bearer";

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = tokenProvider.AccessToken;
        if (!string.IsNullOrEmpty(accessToken))
            request.Headers.Authorization = new AuthenticationHeaderValue(Scheme, accessToken);

        return base.SendAsync(request, cancellationToken);
    }
}
