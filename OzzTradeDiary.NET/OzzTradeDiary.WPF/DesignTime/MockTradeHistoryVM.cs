using TD.AppInfra.DesignTime;
using TD.Models;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.DesignTime
{
    public class MockTradeHistoryVM : TradeHistoryVM
    {
        public MockTradeHistoryVM()
            : base(new EntryOrderMockRepository(), new TakeProfitOrderMockRepository(), new TradeMockRepository(), new StopLossOrderMockRepository(), new MockWindowDialogService(),
                   new SymbolMockLookupService(), new TradingAccountMockLookupService())
        {
            _selectedItem = new Trade
            {
                Id = 1,
                TradingAccountId = 1,
                SymbolId = 1,
                MarketType = MarketType.CryptoPerpetual,
                TradeDirection = TradeDirection.Short,
                TradeStatus = TradeStatus.Closed,
                Tags = "breakout,high-rr",
                EntryTime = DateTime.Now.AddDays(-10),
                ExitTime = DateTime.Now.AddDays(-8),
                PlannedEntryPrice = 75000m,
                ExecutedEntryPrice = 74800m,
                OrderQuantity = 0.02m,
                FilledQuantity = 0.02m,
                PlannedTP = 78000m,
                ExecutedTP = 77950m,
                PlannedSL = 73000m,
                ExecutedSL = 73050m,
                PlannedPositionValue = 1500m,
                ExecutedPositionValue = 1496m,
                RemainingPositionValue = 0m,
                IsFullyClosed = true,
                PlannedProfit = 300m,
                RealizedProfitLoss = 294m,
                PlannedRiskAmount = 200m,
                RealizedRiskAmount = 195m,
                PlannedRiskRewardRatio = 1.5m,
                TotalFeesCalculated = 2.99m,
                FundingFeeTotal = 0.45m,
                NetProfitLoss = 290.56m,
                TradingAccount = new TradingAccount { Id = 1, Title = "Demo Account", MakerFeeRate = 0.0002m, TakerFeeRate = 0.0005m },
                Symbol = new Symbol { Id = 1, Ticker = "BTCUSD", TickerFull = "DEMO:BTCUSD" },
                UpdatedAt = DateTime.Now.AddDays(-8),
            };

            // Entry orders (DCA into position)
            _selectedItem.EntryOrders.Add(new EntryOrder
            {
                Id = 1,
                TradeId = 1,
                OrderType = EntryOrderType.Limit,
                OrderPrice = 75200m,
                OrderQuantity = 0.01m,
                FilledPrice = 75200m,
                FilledQuantity = 0.01m,
                FilledTime = DateTime.Now.AddDays(-10),
                UpdatedAt = DateTime.Now.AddDays(-10)
            });
            _selectedItem.EntryOrders.Add(new EntryOrder
            {
                Id = 2,
                TradeId = 1,
                OrderType = EntryOrderType.Limit,
                OrderPrice = 74400m,
                OrderQuantity = 0.01m,
                FilledPrice = 74400m,
                FilledQuantity = 0.01m,
                FilledTime = DateTime.Now.AddDays(-10).AddHours(2),
                UpdatedAt = DateTime.Now.AddDays(-10)
            });

            // Take profit orders
            _selectedItem.TakeProfitOrders.Add(new TakeProfitOrder
            {
                Id = 1,
                TradeId = 1,
                OrderType = ExitOrderType.Limit,
                OrderPrice = 78000m,
                OrderQuantity = 0.008m,
                FilledPrice = 77950m,
                FilledQuantity = 0.008m,
                FilledTime = DateTime.Now.AddDays(-9),
                UpdatedAt = DateTime.Now.AddDays(-9)
            });
            _selectedItem.TakeProfitOrders.Add(new TakeProfitOrder
            {
                Id = 2,
                TradeId = 1,
                OrderType = ExitOrderType.Limit,
                OrderPrice = 79500m,
                OrderQuantity = 0.006m,
                FilledPrice = 79500m,
                FilledQuantity = 0.006m,
                FilledTime = DateTime.Now.AddDays(-8).AddHours(-6),
                UpdatedAt = DateTime.Now.AddDays(-8)
            });
            _selectedItem.TakeProfitOrders.Add(new TakeProfitOrder
            {
                Id = 3,
                TradeId = 1,
                OrderType = ExitOrderType.TrailingStop,
                OrderPrice = 81000m,
                OrderQuantity = 0.006m,
                UpdatedAt = DateTime.Now.AddDays(-10)
            });

            // Stop loss order
            _selectedItem.StopLossOrders.Add(new StopLossOrder
            {
                Id = 1,
                TradeId = 1,
                OrderType = ExitOrderType.Stop,
                OrderPrice = 73000m,
                OrderQuantity = 0.02m,
                UpdatedAt = DateTime.Now.AddDays(-10)
            });

            ReplaceCollection(EntryOrders, _selectedItem.EntryOrders);
            ReplaceCollection(TakeProfitOrders, _selectedItem.TakeProfitOrders);
            ReplaceCollection(StopLossOrders, _selectedItem.StopLossOrders);

            _selectedItem.SetupNotes = "BTC breaking above key resistance at 75k with strong volume." + Environment.NewLine +
                                       "DCA entry plan: first fill at resistance retest, second fill at 74.4k demand zone.";

            _selectedItem.ReviewNotes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam," +
                "quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
                "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        }
    }
}
