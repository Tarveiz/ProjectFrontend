using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;

namespace FrontTestTask.App.Infrastructure.Converters;

/// <summary>
/// Байты изображения в Bitmap.
/// </summary>
public sealed class BytesToBitmapConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not byte[] { Length: > 0 } bytes)
            return null;

        try
        {
            using var stream = new MemoryStream(bytes);
            return new Bitmap(stream);
        }
        catch (Exception) { return null; }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
}
