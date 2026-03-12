using TD.Models;

namespace TD.SQLite;

public interface IDatabaseSymbolRepository
{
    Task<IReadOnlyList<Symbol>> GetAllAsync(bool? isActive = null);
    Task<Symbol?> GetByTickerFullAsync(string tickerFull);
    Task<Symbol?> GetByIdAsync(int id);
    Task<int> CreateAsync(Symbol symbol);
    Task<bool> UpdateAsync(Symbol symbol);
    Task<bool> DeleteAsync(int id);
}
