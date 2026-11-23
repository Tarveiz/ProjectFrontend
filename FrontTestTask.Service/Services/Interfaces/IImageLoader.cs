namespace FrontTestTask.Service.Services.Interfaces;

/// <summary>
/// Загрузка изображений по URL.
/// </summary>
public interface IImageLoader
{
    public Task<byte[]?> LoadAsync(string url, CancellationToken cancellationToken);
}
