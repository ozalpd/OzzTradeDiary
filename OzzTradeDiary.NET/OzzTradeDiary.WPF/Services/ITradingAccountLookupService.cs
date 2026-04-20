using TD.Models;

namespace TD.WPF.Services;

internal interface ITradingAccountLookupService
{
    Task<IReadOnlyList<TradingAccount>> GetActiveTradingAccountsAsync();
}
