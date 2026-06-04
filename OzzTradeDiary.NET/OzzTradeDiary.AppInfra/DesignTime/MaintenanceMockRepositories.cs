using TD.Helpers;
using TD.Models;
using TD.RepositoryContracts;

namespace TD.AppInfra.DesignTime
{
    public class CurrencyMockRepository : ICurrencyRepository
    {
        public Task<bool> CanDeleteAsync(int id) => Task.FromResult(false);
        public Task<int> CreateAsync(Currency currency) => throw new NotImplementedException();
        public Task<bool> DeleteAsync(int id) => throw new NotImplementedException();
        public Task<IReadOnlyList<Currency>> GetAllAsync(bool? isActive = null) => Task.FromResult<IReadOnlyList<Currency>>(Array.Empty<Currency>());
        public Task<Currency?> GetByCurrencyTickerAsync(string? currencyTicker) => Task.FromResult<Currency?>(null);
        public Task<Currency?> GetByIdAsync(int? id) => Task.FromResult<Currency?>(null);
        public Task<bool> UpdateAsync(Currency currency) => throw new NotImplementedException();
    }

    public class EntryOrderMockRepository : IEntryOrderRepository
    {
        public Task<bool> AnyByTradeIdAsync(int tradeId) => throw new NotImplementedException();
        public Task<bool> CanDeleteAsync(int id) => Task.FromResult(false);
        public Task<int> CreateAsync(EntryOrder entryOrder) => throw new NotImplementedException();
        public Task<bool> DeleteAsync(int id) => throw new NotImplementedException();
        public Task<IReadOnlyList<EntryOrder>> GetAllAsync(bool? isActive = null) => Task.FromResult<IReadOnlyList<EntryOrder>>(Array.Empty<EntryOrder>());
        public Task<IReadOnlyList<EntryOrder>> GetAllAsync() => Task.FromResult<IReadOnlyList<EntryOrder>>(Array.Empty<EntryOrder>());
        public Task<EntryOrder?> GetByIdAsync(int? id) => Task.FromResult<EntryOrder?>(null);
        public Task<IReadOnlyList<EntryOrder>> GetByTradeIdAsync(int tradeId) => Task.FromResult<IReadOnlyList<EntryOrder>>(Array.Empty<EntryOrder>());
        public Task<bool> UpdateAsync(EntryOrder entryOrder) => throw new NotImplementedException();
        public Task<bool> UpdateCancellationTimeAsync(int id, DateTime cancellationTime) => throw new NotImplementedException();
    }

    public class ExchangeMockRepository : IExchangeRepository
    {
        public Task<bool> AnyByDefaultCurrencyIdAsync(int defaultCurrencyId) => Task.FromResult(false);
        public Task<bool> CanDeleteAsync(int id) => Task.FromResult(false);
        public Task<int> CreateAsync(Exchange exchange) => throw new NotImplementedException();
        public Task<bool> DeleteAsync(int id) => throw new NotImplementedException();
        public Task<IReadOnlyList<Exchange>> GetAllAsync(bool? isActive = null) => Task.FromResult<IReadOnlyList<Exchange>>(Array.Empty<Exchange>());
        public Task<IReadOnlyList<Exchange>> GetByDefaultCurrencyIdAsync(int defaultCurrencyId, bool? isActive = null) => Task.FromResult<IReadOnlyList<Exchange>>(Array.Empty<Exchange>());
        public Task<Exchange?> GetByExchangeCodeAsync(string? exchangeCode) => Task.FromResult<Exchange?>(null);
        public Task<Exchange?> GetByIdAsync(int? id) => Task.FromResult<Exchange?>(null);
        public Task<bool> UpdateAsync(Exchange exchange) => throw new NotImplementedException();
        public Task<bool> UpdateHasAnySymbolAsync(int id, bool hasAnySymbol) => throw new NotImplementedException();
        public Task LoadNavigationCollections(Exchange exchange) => Task.CompletedTask;
    }

    public class StopLossOrderMockRepository : IStopLossOrderRepository
    {
        public Task<bool> AnyByTradeIdAsync(int tradeId) => Task.FromResult(false);
        public Task<bool> CanDeleteAsync(int id) => Task.FromResult(false);
        public Task<int> CreateAsync(StopLossOrder stopLossOrder) => throw new NotImplementedException();
        public Task<bool> DeleteAsync(int id) => throw new NotImplementedException();
        public Task<IReadOnlyList<StopLossOrder>> GetAllAsync() => Task.FromResult<IReadOnlyList<StopLossOrder>>(Array.Empty<StopLossOrder>());
        public Task<StopLossOrder?> GetByIdAsync(int? id) => Task.FromResult<StopLossOrder?>(null);
        public Task<IReadOnlyList<StopLossOrder>> GetByTradeIdAsync(int tradeId) => Task.FromResult<IReadOnlyList<StopLossOrder>>(Array.Empty<StopLossOrder>());
        public Task<bool> UpdateAsync(StopLossOrder stopLossOrder) => throw new NotImplementedException();
        public Task<bool> UpdateCancellationTimeAsync(int id, DateTime cancellationTime) => throw new NotImplementedException();
    }

    public class SymbolMockRepository : ISymbolRepository
    {
        public Task<bool> AnyByExchangeIdAsync(int exchangeId) => Task.FromResult(false);
        public Task<bool> AnyByPriceCurrencyIdAsync(int priceCurrencyId) => Task.FromResult(false);
        public Task<bool> CanDeleteAsync(int id) => Task.FromResult(false);
        public Task<int> CreateAsync(Symbol symbol) => throw new NotImplementedException();
        public Task<bool> DeleteAsync(int id) => throw new NotImplementedException();
        public Task<IReadOnlyList<Symbol>> GetAllAsync(bool? isActive = null) => Task.FromResult<IReadOnlyList<Symbol>>(Array.Empty<Symbol>());
        public Task<IReadOnlyList<Symbol>> GetByExchangeIdAsync(int exchangeId, bool? isActive = null) => Task.FromResult<IReadOnlyList<Symbol>>(Array.Empty<Symbol>());
        public Task<IReadOnlyList<Symbol>> GetByPriceCurrencyIdAsync(int priceCurrencyId, bool? isActive = null) => Task.FromResult<IReadOnlyList<Symbol>>(Array.Empty<Symbol>());
        public Task<Symbol?> GetByIdAsync(int? id) => Task.FromResult<Symbol?>(null);
        public Task<Symbol?> GetByTickerFullAsync(string? tickerFull) => Task.FromResult<Symbol?>(null);
        public Task<IReadOnlyList<Symbol>> GetPagedAsync(QueryParameters queryParameters, bool? isActive = null) => Task.FromResult<IReadOnlyList<Symbol>>(Array.Empty<Symbol>());
        public Task<bool> UpdateAsync(Symbol symbol) => throw new NotImplementedException();
        public Task UpdateExchangeHasAnySymbolAsync(int exchangeId) => Task.CompletedTask;
    }

    public class TakeProfitOrderMockRepository : ITakeProfitOrderRepository
    {
        public Task<bool> AnyByTradeIdAsync(int tradeId) => throw new NotImplementedException();
        public Task<bool> CanDeleteAsync(int id) => Task.FromResult(false);
        public Task<int> CreateAsync(TakeProfitOrder takeProfitOrder) => throw new NotImplementedException();
        public Task<bool> DeleteAsync(int id) => throw new NotImplementedException();
        public Task<IReadOnlyList<TakeProfitOrder>> GetAllAsync() => Task.FromResult<IReadOnlyList<TakeProfitOrder>>(Array.Empty<TakeProfitOrder>());
        public Task<TakeProfitOrder?> GetByIdAsync(int? id) => Task.FromResult<TakeProfitOrder?>(null);
        public Task<IReadOnlyList<TakeProfitOrder>> GetByTradeIdAsync(int tradeId) => Task.FromResult<IReadOnlyList<TakeProfitOrder>>(Array.Empty<TakeProfitOrder>());
        public Task<bool> UpdateAsync(TakeProfitOrder takeProfitOrder) => throw new NotImplementedException();
        public Task<bool> UpdateCancellationTimeAsync(int id, DateTime cancellationTime) => throw new NotImplementedException();
    }

    public class TradingAccountMockRepository : ITradingAccountRepository
    {
        public Task<bool> AnyByExchangeIdAsync(int exchangeId) => Task.FromResult(false);
        public Task<bool> CanDeleteAsync(int id) => Task.FromResult(false);
        public Task<int> CreateAsync(TradingAccount tradingAccount) => throw new NotImplementedException();
        public Task<bool> DeleteAsync(int id) => throw new NotImplementedException();
        public Task<IReadOnlyList<TradingAccount>> GetAllAsync(bool? isActive = null) => Task.FromResult<IReadOnlyList<TradingAccount>>(Array.Empty<TradingAccount>());
        public Task<IReadOnlyList<TradingAccount>> GetByExchangeIdAsync(int exchangeId, bool? isActive = null) => Task.FromResult<IReadOnlyList<TradingAccount>>(Array.Empty<TradingAccount>());
        public Task<TradingAccount?> GetByIdAsync(int? id) => Task.FromResult<TradingAccount?>(null);
        public Task<TradingAccount?> GetByTitleAsync(string? title) => Task.FromResult<TradingAccount?>(null);
        public Task<bool> UpdateAsync(TradingAccount tradingAccount) => throw new NotImplementedException();
    }
}
