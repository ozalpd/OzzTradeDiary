using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TD.WPF.Extensions;

internal static class WindowExtensions
{
    /// <summary>
    /// Sets the window icon from a Geometry resource.
    /// </summary>
    /// <param name="window">The window to set the icon for.</param>
    /// <param name="geometryResourceKey">The resource key of the Geometry.</param>
    /// <param name="fillColor">The fill color in hex format (e.g., "#93191C").</param>
    /// <param name="size">The icon size in pixels (default: 16).</param>
    public static void SetIconFromGeometryResource(this Window window, string geometryResourceKey, string fillColor, int size = 16)
    {
        try
        {
            var geometry = (Geometry)window.FindResource(geometryResourceKey);
            var path = new Path
            {
                Data = geometry,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fillColor)),
                Stretch = Stretch.Uniform
            };

            var iconSize = new Size(size, size);
            path.Measure(iconSize);
            path.Arrange(new Rect(iconSize));

            var bitmap = new RenderTargetBitmap(size, size, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(path);
            window.Icon = bitmap;
        }
        catch
        {
            // If icon loading fails, use default icon
        }
    }
}
