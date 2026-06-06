using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TD.WPF.Behaviors;

/// <summary>
/// Attached property that asynchronously loads an image from a local file path or a web URL
/// into an <see cref="Image.Source"/>, avoiding the freeze/download race condition that occurs
/// when using <see cref="BitmapImage.UriSource"/> with HTTP URLs.
/// </summary>
internal static class AsyncImageLoader
{
    private static readonly HttpClient _http = new();

    public static readonly DependencyProperty SourceUrlProperty =
        DependencyProperty.RegisterAttached(
            "SourceUrl", typeof(string), typeof(AsyncImageLoader),
            new PropertyMetadata(null, OnSourceUrlChanged));

    public static void SetSourceUrl(Image image, string? value) => image.SetValue(SourceUrlProperty, value);
    public static string? GetSourceUrl(Image image) => (string?)image.GetValue(SourceUrlProperty);

    private static async void OnSourceUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Image image)
            return;

        image.Source = null;

        var url = e.NewValue as string;
        if (string.IsNullOrWhiteSpace(url))
            return;

        try
        {
            BitmapImage bitmap;

            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                var bytes = await _http.GetByteArrayAsync(url);
                bitmap = LoadFromBytes(bytes);
            }
            else
            {
                if (!File.Exists(url))
                    return;

                var bytes = await File.ReadAllBytesAsync(url);
                bitmap = LoadFromBytes(bytes);
            }

            image.Source = bitmap;
        }
        catch
        {
            // Leave source null on any error
        }
    }

    private static BitmapImage LoadFromBytes(byte[] bytes)
    {
        var bitmap = new BitmapImage();
        bitmap.BeginInit();
        bitmap.CacheOption = BitmapCacheOption.OnLoad;
        bitmap.StreamSource = new MemoryStream(bytes);
        bitmap.EndInit();
        bitmap.Freeze();
        return bitmap;
    }
}
