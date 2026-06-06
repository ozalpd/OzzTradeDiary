using System.ComponentModel;
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
/// Exposes <see cref="ImagePixelWidthProperty"/> and <see cref="ImagePixelHeightProperty"/>
/// read-only attached properties that are populated after a successful load.
/// </summary>
public static class AsyncImageLoader
{
    private static readonly HttpClient _http = new();

    public static readonly DependencyProperty SourceUrlProperty =
        DependencyProperty.RegisterAttached(
            "SourceUrl", typeof(string), typeof(AsyncImageLoader),
            new PropertyMetadata(null, OnSourceUrlChanged));

    public static void SetSourceUrl(Image image, string? value) => image.SetValue(SourceUrlProperty, value);
    public static string? GetSourceUrl(Image image) => (string?)image.GetValue(SourceUrlProperty);

    // --- ImagePixelWidth (read-only) ---
    private static readonly DependencyPropertyKey ImagePixelWidthPropertyKey =
        DependencyProperty.RegisterAttachedReadOnly(
            "ImagePixelWidth", typeof(int), typeof(AsyncImageLoader),
            new PropertyMetadata(0));

    public static readonly DependencyProperty ImagePixelWidthProperty = ImagePixelWidthPropertyKey.DependencyProperty;
    public static int GetImagePixelWidth(Image image) => (int)image.GetValue(ImagePixelWidthProperty);

    // --- ImagePixelHeight (read-only) ---
    private static readonly DependencyPropertyKey ImagePixelHeightPropertyKey =
        DependencyProperty.RegisterAttachedReadOnly(
            "ImagePixelHeight", typeof(int), typeof(AsyncImageLoader),
            new PropertyMetadata(0));

    public static readonly DependencyProperty ImagePixelHeightProperty = ImagePixelHeightPropertyKey.DependencyProperty;
    public static int GetImagePixelHeight(Image image) => (int)image.GetValue(ImagePixelHeightProperty);

    // --- ThumbMaxSize (writable) — the longest edge of the thumbnail in pixels ---
    public static readonly DependencyProperty ThumbMaxSizeProperty =
        DependencyProperty.RegisterAttached(
            "ThumbMaxSize", typeof(double), typeof(AsyncImageLoader),
            new PropertyMetadata(120.0, OnThumbMaxSizeChanged));

    public static void SetThumbMaxSize(Image image, double value) => image.SetValue(ThumbMaxSizeProperty, value);
    public static double GetThumbMaxSize(Image image) => (double)image.GetValue(ThumbMaxSizeProperty);

    private static void OnThumbMaxSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Image image)
            UpdateThumbSize(image);
    }

    // --- ThumbWidth (read-only) ---
    private static readonly DependencyPropertyKey ThumbWidthPropertyKey =
        DependencyProperty.RegisterAttachedReadOnly(
            "ThumbWidth", typeof(double), typeof(AsyncImageLoader),
            new PropertyMetadata(120.0));

    public static readonly DependencyProperty ThumbWidthProperty = ThumbWidthPropertyKey.DependencyProperty;
    public static double GetThumbWidth(Image image) => (double)image.GetValue(ThumbWidthProperty);

    // --- ThumbHeight (read-only) ---
    private static readonly DependencyPropertyKey ThumbHeightPropertyKey =
        DependencyProperty.RegisterAttachedReadOnly(
            "ThumbHeight", typeof(double), typeof(AsyncImageLoader),
            new PropertyMetadata(120.0));

    public static readonly DependencyProperty ThumbHeightProperty = ThumbHeightPropertyKey.DependencyProperty;
    public static double GetThumbHeight(Image image) => (double)image.GetValue(ThumbHeightProperty);

    private static void UpdateThumbSize(Image image)
    {
        int pw = GetImagePixelWidth(image);
        int ph = GetImagePixelHeight(image);
        double maxSize = GetThumbMaxSize(image);

        if (pw <= 0 || ph <= 0)
        {
            // placeholder square while loading
            image.SetValue(ThumbWidthPropertyKey, maxSize);
            image.SetValue(ThumbHeightPropertyKey, maxSize);
            return;
        }

        bool fixedHeigth = true;
        double aspect = (double)pw / ph;
        double w, h;
        if (fixedHeigth || pw <= ph) // landscape or square: constrain width
        {
            h = maxSize;
            w = Math.Round(maxSize * aspect);
        }
        else           // portrait: constrain height
        {
            w = maxSize;
            h = Math.Round(maxSize / aspect);
        }
        image.SetValue(ThumbWidthPropertyKey, w);
        image.SetValue(ThumbHeightPropertyKey, h);
    }

    private static async void OnSourceUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Image image)
            return;

        if (DesignerProperties.GetIsInDesignMode(image))
            return;

        image.Source = null;
        image.SetValue(ImagePixelWidthPropertyKey, 0);
        image.SetValue(ImagePixelHeightPropertyKey, 0);
        UpdateThumbSize(image);

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
            image.SetValue(ImagePixelWidthPropertyKey, bitmap.PixelWidth);
            image.SetValue(ImagePixelHeightPropertyKey, bitmap.PixelHeight);
            UpdateThumbSize(image);
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
