using TD.AppInfra.DesignTime;
using TD.Models;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.DesignTime
{
    public class MockTradeHistoryVM : TradeHistoryVM
    {
        public MockTradeHistoryVM()
            : base(new TradeMockRepository(), new MockWindowDialogService(),
                   new SymbolMockLookupService(), new TradingAccountMockLookupService())
        {
            _selectedItem = new Trade
            {
                Id = 1,
                EntryMethod = EntryMethod.Limit,
                TradeDirection = TradeDirection.Long,
                EntryTime = DateTime.Now.AddDays(-10),
                ExitTime = DateTime.Now,
                PlannedEntryPrice = 75000m,
                ExecutedEntryPrice = 74800m,
                OrderQuantity = 0.01m,
                FilledQuantity = 0.01m,
                PlannedTP = 78000m,
                PlannedSL = 73000m,
                SetupNotes = "Design-time sample trade.",
                TradingAccount = new TradingAccount { Id = 1, Title = "Demo Account" },
                Symbol = new Symbol { Id = 1, Ticker = "BTCUSD", TickerFull = "BTCUSD" },
                UpdatedAt = DateTime.Now.AddDays(-1),
            };

            _selectedItem.SetupNotes += Environment.NewLine + "This is a longer note to demonstrate multi-line text display in the UI.";

            _selectedItem.ReviewNotes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam," +
                "quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
                "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        }
    }
}
