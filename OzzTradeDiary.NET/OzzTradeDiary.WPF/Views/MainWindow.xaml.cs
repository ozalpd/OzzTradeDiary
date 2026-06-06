using System.Windows;
using System.Windows.Controls;
using TD.AppInfra.DesignTime;
using TD.AppInfra.Models;
using TD.Models;
using TD.WPF.DesignTime;
using TD.WPF.Models;
using TD.WPF.ViewModels;
using TD.WPF.Views.Trades;

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

        private void ClearComboBox_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button)
                return;

            var tag = button.Tag as string;
            if (string.IsNullOrWhiteSpace(tag))
                return;

            switch (tag)
            {
                case "ClearAccount":
                    _viewModel.TradeHistory.QueryVM.ByTradingAccountId = null;
                    break;
                case "ClearDirection":
                    _viewModel.TradeHistory.QueryVM.ByTradeDirection = null;
                    break;
                case "ClearSymbol":
                    _viewModel.TradeHistory.QueryVM.BySymbolId = null;
                    break;
            }

        }

        private void TradeImageThumbnail_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn || btn.Tag is not TradeImage tradeImage)
                return;

            if (_viewModel.TradeHistory.SelectedTradeImage != tradeImage)
            {
                _viewModel.TradeHistory.SelectedTradeImage = tradeImage;
                _lastClickToImgButton = DateTime.Now;
                return;
            }

            //Is it a double click?
            var timeSinceLastClick = DateTime.Now - _lastClickToImgButton;
            if (timeSinceLastClick.TotalMilliseconds > 500)
            {
                _lastClickToImgButton = DateTime.Now;
                return; // Not a double click, just update the timestamp and return.
            }

            var detailView = new TradeImageDetailView(tradeImage) { Owner = this };
            detailView.ShowDialog();
        }
        private DateTime _lastClickToImgButton;
    }
}