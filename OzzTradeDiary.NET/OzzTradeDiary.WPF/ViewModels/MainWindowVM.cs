using TD.AppInfra.Models;
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

        public MainWindowVM(AppDataSources dataSources)
            : base(dataSources.CurrencyRepository, dataSources.ExchangeRepository,
                   dataSources.SymbolRepository, dataSources.TradingAccountRepository)
        {
            TradeRepository = dataSources.TradeRepository;

            var symbolLookupService = new SymbolLookupService(dataSources.SymbolRepository);
            var tradingAccountLookupService = new TradingAccountLookupService(dataSources.TradingAccountRepository);

            TradeHistory = new TradeHistoryVM(dataSources.TradeRepository, _windowDialogService, symbolLookupService, tradingAccountLookupService);
            ShowMaintenanceCommand = new ShowMaintenanceCommand(dataSources);
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
