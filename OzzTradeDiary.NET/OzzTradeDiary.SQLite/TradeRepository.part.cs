using TD.Helpers;
using TD.Models;

namespace TD.SQLite
{
    public partial class TradeRepository
    {
        public async Task<IReadOnlyList<Trade>> GetPagedAsync(TradeQueryParameters queryParameters)
        {
            ArgumentNullException.ThrowIfNull(queryParameters);

            var result = new List<Trade>();
            var tradingAccountsById = (await _tradingAccountRepository.GetAllAsync()).ToDictionary(item => item.Id);
            var symbolsById = (await _symbolRepository.GetAllAsync()).ToDictionary(item => item.Id);

            await using var connection = await GetOpenConnectionAsync();
            await using var command = connection.CreateCommand();
            command.CommandText = _selectStatement;

            var whereClauses = new List<string>();
            if (queryParameters.TradingAccountId.HasValue)
            {
                whereClauses.Add("TradingAccountId = @tradingAccountId");
                command.Parameters.AddWithValue("@tradingAccountId", queryParameters.TradingAccountId.Value);
            }
            if (queryParameters.SymbolId.HasValue)
            {
                whereClauses.Add("SymbolId = @symbolId");
                command.Parameters.AddWithValue("@symbolId", queryParameters.SymbolId.Value);
            }
            if (queryParameters.TradeDirection.HasValue)
            {
                whereClauses.Add("TradeDirection = @tradeDirection");
                command.Parameters.AddWithValue("@tradeDirection", (int)queryParameters.TradeDirection.Value);
            }
            if (queryParameters.EntryTimeMin.HasValue)
            {
                whereClauses.Add("datetime(EntryTime) >= datetime(@entryTimeMin)");
                command.Parameters.AddWithValue("@entryTimeMin", queryParameters.EntryTimeMin.Value.ToUniversalTime().ToString("O"));
            }
            if (queryParameters.EntryTimeMax.HasValue)
            {
                whereClauses.Add("datetime(EntryTime) <= datetime(@entryTimeMax)");
                command.Parameters.AddWithValue("@entryTimeMax", queryParameters.EntryTimeMax.Value.ToUniversalTime().ToString("O"));
            }

            if (queryParameters.ExecutedPositionValueMin.HasValue)
            {
                whereClauses.Add("(ExecutedEntry * FilledQuantity) >= @executedPositionValueMin");
                command.Parameters.AddWithValue("@executedPositionValueMin", queryParameters.ExecutedPositionValueMin.Value);
            }
            if (queryParameters.ExecutedPositionValueMax.HasValue)
            {
                whereClauses.Add("(ExecutedEntry * FilledQuantity) <= @executedPositionValueMax");
                command.Parameters.AddWithValue("@executedPositionValueMax", queryParameters.ExecutedPositionValueMax.Value);
            }
            if (queryParameters.PlannedPositionValueMin.HasValue)
            {
                whereClauses.Add("(PlannedEntry * OrderQuantity) >= @plannedPositionValueMin");
                command.Parameters.AddWithValue("@plannedPositionValueMin", queryParameters.PlannedPositionValueMin.Value);
            }
            if (queryParameters.PlannedPositionValueMax.HasValue)
            {
                whereClauses.Add("(PlannedEntry * OrderQuantity) <= @plannedPositionValueMax");
                command.Parameters.AddWithValue("@plannedPositionValueMax", queryParameters.PlannedPositionValueMax.Value);
            }

            if (queryParameters.UpdatedAtMin.HasValue)
            {
                whereClauses.Add("datetime(UpdatedAt) >= datetime(@updatedAtMin)");
                command.Parameters.AddWithValue("@updatedAtMin", queryParameters.UpdatedAtMin.Value.ToUniversalTime().ToString("O"));
            }
            if (queryParameters.UpdatedAtMax.HasValue)
            {
                whereClauses.Add("datetime(UpdatedAt) <= datetime(@updatedAtMax)");
                command.Parameters.AddWithValue("@updatedAtMax", queryParameters.UpdatedAtMax.Value.ToUniversalTime().ToString("O"));
            }

            if (whereClauses.Count > 0)
            {
                var whereClause = string.Join(" AND ", whereClauses);
                command.CommandText = _selectStatement + " WHERE " + whereClause;
            }

            command.CommandText += " ORDER BY Id LIMIT @pageSize OFFSET @skip";
            command.Parameters.AddWithValue("@pageSize", queryParameters.PageSize);
            command.Parameters.AddWithValue("@skip", queryParameters.Skip);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var trade = MapTrade(reader);
                if (tradingAccountsById.TryGetValue(trade.TradingAccountId, out var tradingAccount))
                    trade.TradingAccount = tradingAccount;

                if (symbolsById.TryGetValue(trade.SymbolId, out var symbol))
                    trade.Symbol = symbol;

                result.Add(trade);
            }

            return result;
        }

        public async Task LoadNavigationCollections(Trade trade)
        {
            try
            {
                var images = await _tradeImageRepository.GetByTradeIdAsync(trade.Id);
                trade.TradeImages = images.ToList();
            }
            catch (Exception)
            {
                // handle/log as needed
            }

            try
            {
                var entryOrders = await _entryOrderRepository.GetByTradeIdAsync(trade.Id);
                trade.EntryOrders = entryOrders.ToList();
            }
            catch (Exception)
            {
                // handle/log as needed
            }

            try
            {
                var tpOrders = await _takeProfitOrderRepository.GetByTradeIdAsync(trade.Id);
                trade.TakeProfitOrders = tpOrders.ToList();
            }
            catch (Exception)
            {
                // handle/log as needed
            }

            try
            {
                var slOrders = await _stopLossOrderRepository.GetByTradeIdAsync(trade.Id);
                trade.StopLossOrders = slOrders.ToList();
            }
            catch (Exception)
            {
                // handle/log as needed
            }
        }
        partial void OnLoaded(Trade trade)
        {
            _ = LoadNavigationCollections(trade);
        }
    }

    public partial interface ITradeRepository
    {
        Task LoadNavigationCollections(Trade trade);
        Task<IReadOnlyList<Trade>> GetPagedAsync(TradeQueryParameters queryParameters);
    }
}