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
        private ISymbolLookupService _symbolLookupService = new SymbolMockLookupService();
        private ITradingAccountLookupService _tradingAccountLookupService = new TradingAccountMockLookupService();

        public MainWindowVM(ITradeRepository tradeRepository)
        {
            TradeRepository = tradeRepository ?? throw new ArgumentNullException(nameof(tradeRepository));

            TradeHistory = new TradeHistoryVM(tradeRepository, _windowDialogService, _symbolLookupService, _tradingAccountLookupService);
            ShowMaintenanceCommand = new ShowMaintenanceCommand();
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
