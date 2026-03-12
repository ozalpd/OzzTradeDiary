using TD.Models;

namespace TD.SQLite;

public interface IDatabaseCurrencyRepository
{
    Task<IReadOnlyList<Currency>> GetAllAsync(bool? isActive = null);
    Task<Currency?> GetByCurrencyTickerAsync(string currencyTicker);
    Task<Currency?> GetByIdAsync(int id);
    Task<int> CreateAsync(Currency currency);
    Task<bool> UpdateAsync(Currency currency);
    Task<bool> DeleteAsync(int id);
}
