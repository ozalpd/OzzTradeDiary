using TD.AppInfra.Services;
using TD.RepositoryContracts;
using TD.WPF.Services;

namespace TD.WPF.ViewModels.Trades
{
    public class TradeHistoryVM : TradeListVM
    {
        public TradeHistoryVM(ITradeRepository tradeRepository, IWindowDialogService windowDialogService,
                              ISymbolLookupService symbolLookupService, ITradingAccountLookupService tradingAccountLookupService)
            : base(tradeRepository, windowDialogService, symbolLookupService, tradingAccountLookupService)
        {
        }
    }
}
