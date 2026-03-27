using TD.Models;

namespace TD.SQLite;

public interface IDbTradingAccountRepository
{
    Task<IReadOnlyList<TradingAccount>> GetAllAsync(bool? isActive = null);
    Task<TradingAccount?> GetByTitleAsync(string title);
    Task<TradingAccount?> GetByIdAsync(int id);
    Task<int> CreateAsync(TradingAccount tradingAccount);
    Task<bool> UpdateAsync(TradingAccount tradingAccount);
    Task<bool> DeleteAsync(int id);
}
