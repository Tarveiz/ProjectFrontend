using FrontTestTask.Service.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace FrontTestTask.Service.Services;

public sealed class ImageLoader(HttpClient httpClient, ILogger<ImageLoader> logger) : IImageLoader
{
    public async Task<byte[]?> LoadAsync(string url, CancellationToken cancellationToken)
    {
        try
        {
            return await httpClient.GetByteArrayAsync(url, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { return null; }
        catch (Exception ex) when (ex is HttpRequestException or OperationCanceledException)
        {
            logger.LogWarning(ex, "Failed to load image {Url}", url);
            return null;
        }
    }
}
