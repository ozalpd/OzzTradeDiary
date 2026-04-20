using TD.Models;

namespace TD.WPF.Services;

internal sealed class EmptyExchangeLookupService : IExchangeLookupService
{
    public Task<IReadOnlyList<Exchange>> GetActiveExchangesAsync()
    {
        return Task.FromResult<IReadOnlyList<Exchange>>(Array.Empty<Exchange>());
    }
}

internal sealed class EmptyCurrencyLookupService : ICurrencyLookupService
{
    public Task<IReadOnlyList<Currency>> GetActiveCurrenciesAsync()
    {
        return Task.FromResult<IReadOnlyList<Currency>>(Array.Empty<Currency>());
    }
}

internal sealed class EmptySymbolLookupService : ISymbolLookupService
{
    public Task<IReadOnlyList<Symbol>> GetActiveSymbolsAsync()
    {
        return Task.FromResult<IReadOnlyList<Symbol>>(Array.Empty<Symbol>());
    }
}

internal sealed class EmptyTradingAccountLookupService : ITradingAccountLookupService
{
    public Task<IReadOnlyList<TradingAccount>> GetActiveTradingAccountsAsync()
    {
        return Task.FromResult<IReadOnlyList<TradingAccount>>(Array.Empty<TradingAccount>());
    }
}
