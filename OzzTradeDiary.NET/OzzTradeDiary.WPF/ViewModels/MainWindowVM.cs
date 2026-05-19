using TD.AppInfra.DesignTime;
using TD.AppInfra.Services;
using TD.RepositoryContracts;
using TD.WPF.Commands;
using TD.WPF.Services;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.ViewModels
{
    internal class MainWindowVM : AbstractDiaryVM
    {
        private readonly IWindowDialogService _windowDialogService = new WindowDialogService();

        public MainWindowVM(ITradeRepository tradeRepository,
                            ICurrencyRepository currencyRepository,
                            IExchangeRepository exchangeRepository,
                            ISymbolRepository symbolRepository,
                            ITradingAccountRepository tradingAccountRepository)
            : base(currencyRepository, exchangeRepository, symbolRepository, tradingAccountRepository)
        {
            TradeRepository = tradeRepository ?? throw new ArgumentNullException(nameof(tradeRepository));

            var symbolLookupService = new SymbolLookupService(symbolRepository);
            var tradingAccountLookupService = new TradingAccountLookupService(tradingAccountRepository);

            TradeHistory = new TradeHistoryVM(tradeRepository, _windowDialogService, symbolLookupService, tradingAccountLookupService);
            ShowMaintenanceCommand = new ShowMaintenanceCommand(currencyRepository, exchangeRepository, symbolRepository, tradingAccountRepository);
            ShowAboutCommand = new ShowAboutCommand();
            ExitCommand = new ExitCommand();
        }

        public ITradeRepository TradeRepository { get; }
        public TradeHistoryVM TradeHistory { get; }
        public ShowMaintenanceCommand ShowMaintenanceCommand { get; }
        public ShowAboutCommand ShowAboutCommand { get; }
        public ExitCommand ExitCommand { get; }
    }
}
