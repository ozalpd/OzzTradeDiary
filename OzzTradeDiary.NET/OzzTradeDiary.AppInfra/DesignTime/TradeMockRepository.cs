using TD.Helpers;
using TD.Models;
using TD.RepositoryContracts;

namespace TD.AppInfra.DesignTime
{
    public class TradeMockRepository : ITradeRepository
    {
        private static readonly IReadOnlyList<Trade> _trades = CreateMockTrades();

        private static IReadOnlyList<Trade> CreateMockTrades()
        {
            var exchange = new Exchange { Id = 1, ExchangeCode = "DEMO", ExchangeName = "Demo Exchange" };
            var account = new TradingAccount { Id = 1, Title = "Demo Account", ExchangeId = 1, Exchange = exchange };
            var symbols = new[]
            {
                new Symbol { Id = 1, Ticker = "BTCUSD", TickerFull = "BTCUSD", ExchangeId = 1, Exchange = exchange },
                new Symbol { Id = 2, Ticker = "ETHUSD", TickerFull = "ETHUSD", ExchangeId = 1, Exchange = exchange },
                new Symbol { Id = 3, Ticker = "SOLUSD", TickerFull = "SOLUSD", ExchangeId = 1, Exchange = exchange },
                new Symbol { Id = 4, Ticker = "AVAXUSD", TickerFull = "AVAXUSD", ExchangeId = 1, Exchange = exchange },
            };
            var prices = new[] { 75000m, 2500m, 100m, 10m };
            var random = new Random(42);
            var trades = new List<Trade>();

            for (int i = 0; i < symbols.Length; i++)
            {
                var symbol = symbols[i];
                var price = prices[i];
                var direction = i % 2 == 0 ? TradeDirection.Long : TradeDirection.Short;
                decimal tpMult = direction == TradeDirection.Long ? 1.04m : 0.96m;
                decimal slMult = direction == TradeDirection.Long ? 0.98m : 1.02m;
                decimal qty = 100m / price;

                var trade = new Trade
                {
                    Id = i + 1,
                    TradingAccountId = account.Id,
                    TradingAccount = account,
                    SymbolId = symbol.Id,
                    Symbol = symbol,
                    TradeDirection = direction,
                    PlannedEntryPrice = price,
                    ExecutedEntryPrice = price,
                    PlannedTP = price * tpMult,
                    PlannedSL = price * slMult,
                    OrderQuantity = qty,
                    FilledQuantity = qty,
                    EntryTime = DateTime.UtcNow.AddDays(-(i + 1)),
                    UpdatedAt = DateTime.UtcNow,
                };
                trades.Add(trade);
            }
            return trades;
        }

        public Task<IReadOnlyList<Trade>> GetAllAsync()
        {
            return Task.FromResult(_trades);
        }

        public Task<IReadOnlyList<Trade>> GetPagedAsync(TradeQueryParameters queryParameters)
        {
            var page = queryParameters.Page > 0 ? queryParameters.Page : 1;
            var pageSize = queryParameters.PageSize > 0 ? queryParameters.PageSize : 20;
            IReadOnlyList<Trade> result = _trades.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Task.FromResult(result);
        }

        public Task<bool> AnyBySymbolIdAsync(int symbolId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyByTradingAccountIdAsync(int tradingAccountId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CanDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync(Trade trade)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Trade?> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Trade>> GetBySymbolIdAsync(int symbolId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Trade>> GetByTradingAccountIdAsync(int tradingAccountId)
        {
            throw new NotImplementedException();
        }

        public Task LoadNavigationCollectionsAsync(Trade trade)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Trade trade)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(TradeQueryParameters queryParameters)
        {
            return Task.FromResult(5);
        }

        public Task SaveNavigationCollectionsAsync(Trade trade)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntryTimeAsync(int id, DateTime entryTime)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateExitTimeAsync(int id, DateTime exitTime)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateReviewNotesAsync(int id, string reviewNotes)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSetupNotesAsync(int id, string setupNotes)
        {
            throw new NotImplementedException();
        }
    }
}
