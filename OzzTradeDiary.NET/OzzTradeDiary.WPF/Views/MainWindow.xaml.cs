using System.Windows;
using TD.AppInfra.DesignTime;
using TD.RepositoryContracts;
using TD.WPF.Models;
using TD.WPF.ViewModels;

namespace TD.WPF.Views
{
    public partial class MainWindow : Window
    {
        private readonly AppSettings _appSettings = AppSettings.GetAppSettings();
        private readonly ITradeRepository _tradeRepository;
        private MainWindowVM _viewModel;

        /// <summary>
        /// Parameterless constructor for the XAML designer.
        /// </summary>
        public MainWindow() : this(new TradeMockRepository())
        {
        }

        public MainWindow(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository ?? throw new ArgumentNullException(nameof(tradeRepository));
            InitializeComponent();
            SourceInitialized += MainWindow_SourceInitialized;
            Closing += MainWindow_Closing;
        }

        private async void MainWindow_SourceInitialized(object? sender, EventArgs e)
        {
            SourceInitialized -= MainWindow_SourceInitialized;
            _appSettings.MainWindowPosition.SetWindowPositions(this);
            Title = $"Ozz Trade Diary - v{AppVersion.Version}";
            _viewModel = new MainWindowVM(_tradeRepository);
            DataContext = _viewModel;
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _appSettings.MainWindowPosition.GetWindowPositions(this);
            _appSettings.Save();
        }
    }
}