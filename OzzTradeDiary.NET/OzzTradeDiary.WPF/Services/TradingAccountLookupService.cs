using TD.Models;
using TD.SQLite;

namespace TD.WPF.Services;

internal class TradingAccountLookupService : ITradingAccountLookupService
{
    private readonly ITradingAccountRepository _tradingAccountRepository;

    public TradingAccountLookupService(ITradingAccountRepository tradingAccountRepository)
    {
        _tradingAccountRepository = tradingAccountRepository;
    }

    public async Task<IReadOnlyList<TradingAccount>> GetActiveTradingAccountsAsync()
    {
        var tradingAccounts = await _tradingAccountRepository.GetAllAsync(isActive: true);
        return tradingAccounts.ToList();
    }
}
