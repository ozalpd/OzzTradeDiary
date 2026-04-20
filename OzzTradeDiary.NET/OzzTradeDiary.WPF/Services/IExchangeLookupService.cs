using TD.Models;

namespace TD.WPF.Services;

internal interface IExchangeLookupService
{
    Task<IReadOnlyList<Exchange>> GetActiveExchangesAsync();
}
