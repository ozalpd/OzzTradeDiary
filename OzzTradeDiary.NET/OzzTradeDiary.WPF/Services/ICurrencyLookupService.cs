using TD.Models;

namespace TD.WPF.Services;

internal interface ICurrencyLookupService
{
    Task<IReadOnlyList<Currency>> GetActiveCurrenciesAsync();
}
