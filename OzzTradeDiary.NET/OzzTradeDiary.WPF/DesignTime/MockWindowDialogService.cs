using System.Windows;
using TD.AppInfra.Services;
using TD.Models;
using TD.WPF.Services;

namespace TD.WPF.DesignTime
{
    public class MockWindowDialogService : IWindowDialogService
    {
        public (bool IsConfirmed, Currency? Currency) ShowCurrencyCreateDialog(Window owner) => (false, null);
        public (bool IsConfirmed, bool IsDirty) ShowCurrencyEditDialog(Window owner, Currency currency) => (false, false);
        public (bool IsConfirmed, Exchange? Exchange) ShowExchangeCreateDialog(Window owner, ICurrencyLookupService currencyLookupService) => (false, null);
        public (bool IsConfirmed, bool IsDirty) ShowExchangeEditDialog(Window owner, Exchange exchange) => (false, false);
        public (bool IsConfirmed, Symbol? Symbol) ShowSymbolCreateDialog(Window owner, ICurrencyLookupService currencyLookupService, IExchangeLookupService exchangeLookupService, Exchange? preselectedExchange) => (false, null);
        public (bool IsConfirmed, bool IsDirty) ShowSymbolEditDialog(Window owner, Symbol symbol) => (false, false);
        public (bool IsConfirmed, Trade? Trade) ShowTradeCreateDialog(Window owner, ISymbolLookupService symbolLookupService, ITradingAccountLookupService tradingAccountLookupService, TradingAccount? preselectedTradingAccount) => (false, null);
        public (bool IsConfirmed, bool IsDirty) ShowTradeEditDialog(Window owner, Trade trade) => (false, false);
        public void ShowTradeDetailDialog(Window owner, Trade trade) { }
        public (bool IsConfirmed, TradingAccount? TradingAccount) ShowTradingAccountCreateDialog(Window owner, IExchangeLookupService exchangeLookupService, Exchange? preselectedExchange) => (false, null);
        public (bool IsConfirmed, bool IsDirty) ShowTradingAccountEditDialog(Window owner, TradingAccount tradingAccount) => (false, false);
        public (bool IsConfirmed, EntryOrder? EntryOrder) ShowEntryOrderCreateDialog(Window owner, Trade? preselectedTrade) => (false, null);
        public (bool IsConfirmed, bool IsDirty) ShowEntryOrderEditDialog(Window owner, EntryOrder entryOrder) => (false, false);
        public (bool IsConfirmed, StopLossOrder? StopLossOrder) ShowStopLossOrderCreateDialog(Window owner, Trade? preselectedTrade) => (false, null);
        public (bool IsConfirmed, bool IsDirty) ShowStopLossOrderEditDialog(Window owner, StopLossOrder stopLossOrder) => (false, false);
        public (bool IsConfirmed, TakeProfitOrder? TakeProfitOrder) ShowTakeProfitOrderCreateDialog(Window owner, Trade? preselectedTrade) => (false, null);
        public (bool IsConfirmed, bool IsDirty) ShowTakeProfitOrderEditDialog(Window owner, TakeProfitOrder takeProfitOrder) => (false, false);
        public (bool IsConfirmed, TradeImage? TradeImage) ShowTradeImageCreateDialog(Window owner, Trade? preselectedTrade) => (false, null);
        public (bool IsConfirmed, bool IsDirty) ShowTradeImageEditDialog(Window owner, TradeImage tradeImage) => (false, false);
    }
}
