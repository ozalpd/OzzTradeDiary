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
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IExchangeRepository _exchangeRepository;
        private readonly ISymbolRepository _symbolRepository;
        private readonly ITradingAccountRepository _tradingAccountRepository;
        private MainWindowVM _viewModel;

        /// <summary>
        /// Parameterless constructor for the XAML designer.
        /// </summary>
        public MainWindow() : this(
            new TradeMockRepository(),
            new CurrencyMockRepository(), new ExchangeMockRepository(),
            new SymbolMockRepository(), new TradingAccountMockRepository())
        {
        }

        public MainWindow(ITradeRepository tradeRepository,
                          ICurrencyRepository currencyRepository,
                          IExchangeRepository exchangeRepository,
                          ISymbolRepository symbolRepository,
                          ITradingAccountRepository tradingAccountRepository)
        {
            _tradeRepository = tradeRepository ?? throw new ArgumentNullException(nameof(tradeRepository));
            _currencyRepository = currencyRepository ?? throw new ArgumentNullException(nameof(currencyRepository));
            _exchangeRepository = exchangeRepository ?? throw new ArgumentNullException(nameof(exchangeRepository));
            _symbolRepository = symbolRepository ?? throw new ArgumentNullException(nameof(symbolRepository));
            _tradingAccountRepository = tradingAccountRepository ?? throw new ArgumentNullException(nameof(tradingAccountRepository));
            InitializeComponent();
            SourceInitialized += MainWindow_SourceInitialized;
            Closing += MainWindow_Closing;
        }

        private async void MainWindow_SourceInitialized(object? sender, EventArgs e)
        {
            SourceInitialized -= MainWindow_SourceInitialized;
            _appSettings.MainWindowPosition.SetWindowPositions(this);
            Title = $"Ozz Trade Diary - v{AppVersion.Version}";
            _viewModel = new MainWindowVM(_tradeRepository,
                                          _currencyRepository, _exchangeRepository,
                                          _symbolRepository, _tradingAccountRepository);
            DataContext = _viewModel;
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _appSettings.MainWindowPosition.GetWindowPositions(this);
            _appSettings.Save();
        }
    }
}