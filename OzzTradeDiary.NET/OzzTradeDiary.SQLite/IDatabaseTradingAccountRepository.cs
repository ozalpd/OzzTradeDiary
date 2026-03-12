using TD.Models;

namespace TD.SQLite;

public interface IDatabaseTradingAccountRepository
{
    Task<IReadOnlyList<TradingAccount>> GetAllAsync(bool? isActive = null);
    Task<TradingAccount?> GetByAccountCodeAsync(string accountCode);
    Task<TradingAccount?> GetByIdAsync(int id);
    Task<int> CreateAsync(TradingAccount tradingAccount);
    Task<bool> UpdateAsync(TradingAccount tradingAccount);
    Task<bool> DeleteAsync(int id);
}
