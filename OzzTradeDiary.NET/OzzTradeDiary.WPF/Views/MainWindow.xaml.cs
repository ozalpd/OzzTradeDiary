using System.Windows;
using TD.AppInfra.DesignTime;
using TD.AppInfra.Models;
using TD.Models;
using TD.WPF.DesignTime;
using TD.WPF.Models;
using TD.WPF.ViewModels;

namespace TD.WPF.Views
{
    public partial class MainWindow : Window
    {
        private readonly AppSettings _appSettings = AppSettings.GetAppSettings();
        private readonly AppDataSources _dataSources;
        private MainWindowVM _viewModel;

        /// <summary>
        /// Parameterless constructor for the XAML designer.
        /// </summary>
        public MainWindow() : this(new MockDataSources()) { }

        public MainWindow(AppDataSources dataSources)
        {
            _dataSources = dataSources ?? throw new ArgumentNullException(nameof(dataSources));
            InitializeComponent();
            SourceInitialized += MainWindow_SourceInitialized;
            Closing += MainWindow_Closing;
        }

        private async void MainWindow_SourceInitialized(object? sender, EventArgs e)
        {
            SourceInitialized -= MainWindow_SourceInitialized;
            _appSettings.MainWindowPosition.SetWindowPositions(this);
            Title = $"Ozz Trade Diary - v{AppVersion.Version}";
            _viewModel = new MainWindowVM(_dataSources);
            DataContext = _viewModel;
            await _viewModel.TradeHistory.InitializeAsync();
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _appSettings.MainWindowPosition.GetWindowPositions(this);
            _appSettings.Save();
        }
    }
}