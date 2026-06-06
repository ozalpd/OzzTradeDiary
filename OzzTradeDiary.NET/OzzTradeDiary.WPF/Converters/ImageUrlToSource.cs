using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TD.WPF.Converters;

/// <summary>
/// Converts a <see cref="string"/> containing a local file path or a web URL
/// to a <see cref="BitmapImage"/> suitable for binding to an <see cref="System.Windows.Controls.Image"/>.
/// Returns <c>null</c> for empty or invalid values.
/// </summary>
internal class ImageUrlToSource : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string url || string.IsNullOrWhiteSpace(url))
            return null;

        try
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;

            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                bitmap.UriSource = new Uri(url, UriKind.Absolute);
            }
            else if (File.Exists(url))
            {
                bitmap.UriSource = new Uri(url, UriKind.Absolute);
            }
            else
            {
                return null;
            }

            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }
        catch
        {
            return null;
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
