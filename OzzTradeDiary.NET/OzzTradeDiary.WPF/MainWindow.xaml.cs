using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TD.WPF.Models;

namespace TD.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AppSettings _appSettings = AppSettings.GetAppSettings();

        public MainWindow()
        {
            InitializeComponent();
            SourceInitialized += MainWindow_SourceInitialized;
            Closing += MainWindow_Closing;
        }

        private void MainWindow_SourceInitialized(object? sender, EventArgs e)
        {
            SourceInitialized -= MainWindow_SourceInitialized;
            _appSettings.MainWindowPosition.SetWindowPositions(this);
            Title = $"Ozz Trade Diary - v{AppVersion.Version}";
        }

        private async void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _appSettings.MainWindowPosition.GetWindowPositions(this);
            _appSettings.Save();
        }
    }
}