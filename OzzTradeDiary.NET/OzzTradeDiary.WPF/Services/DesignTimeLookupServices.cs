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
