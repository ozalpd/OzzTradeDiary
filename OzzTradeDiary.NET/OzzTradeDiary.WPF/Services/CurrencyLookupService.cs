using TD.Models;
using TD.SQLite;

namespace TD.WPF.Services;

internal class CurrencyLookupService : ICurrencyLookupService
{
    private readonly ICurrencyRepository _currencyRepository;

    public CurrencyLookupService(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<IReadOnlyList<Currency>> GetActiveCurrenciesAsync()
    {
        var currencies = await _currencyRepository.GetAllAsync(isActive: true);
        return currencies.ToList();
    }
}
