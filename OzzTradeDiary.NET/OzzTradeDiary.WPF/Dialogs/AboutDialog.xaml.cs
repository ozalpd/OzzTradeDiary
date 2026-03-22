using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using TD.WPF.Models;

namespace TD.WPF.Dialogs
{
    /// <summary>
    /// Interaction logic for AboutDialog.xaml
    /// </summary>
    public partial class AboutDialog : Window
    {
        public string Product => AppVersion.Product;
        public string Version => AppVersion.Version;
        public string Description => AppVersion.Description;
        public string Copyright => AppVersion.Copyright;

        public AboutDialog()
        {
            InitializeComponent();
            DataContext = this;
            LoadHighResolutionIcon();
            Deactivated += AboutDialog_Deactivated;
            Closing += AboutDialog_Closing;
        }

        private void AboutDialog_Deactivated(object? sender, EventArgs e)
        {
            if (!_isClosimg)
                Close();
        }
        bool _isClosimg = false;

        private void AboutDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _isClosimg = true;
        }


        private void LoadHighResolutionIcon()
        {
            try
            {
                var iconUri = new Uri("pack://application:,,,/Assets/OzzTradeDiary-256.ico");
                var decoder = BitmapDecoder.Create(iconUri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

                // Find the largest frame (256x256 or 128x128)
                BitmapFrame? bestFrame = null;
                int maxSize = 0;

                foreach (var frame in decoder.Frames)
                {
                    int size = frame.PixelWidth * frame.PixelHeight;
                    if (size > maxSize)
                    {
                        maxSize = size;
                        bestFrame = frame;
                    }
                }

                if (bestFrame != null)
                {
                    AppIcon.Source = bestFrame;
                }
            }
            catch
            {
                // If loading fails, keep the default XAML source
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = e.Uri.AbsoluteUri,
                    UseShellExecute = true
                });
                e.Handled = true;
            }
            catch
            {
                // Silently fail if browser cannot be opened
            }
        }
    }
}
