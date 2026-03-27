using TD.Models;

namespace TD.SQLite;

public interface IDbExchangeRepository
{
    Task<IReadOnlyList<Exchange>> GetAllAsync(bool? isActive = null);
    Task<Exchange?> GetByExchangeCodeAsync(string exchangeCode);
    Task<Exchange?> GetByIdAsync(int id);
    Task<int> CreateAsync(Exchange exchange);
    Task<bool> UpdateAsync(Exchange exchange);
    Task<bool> SetHasAnySymbol(int exchangeId, bool hasAnySymbol);
    Task<bool> DeleteAsync(int id);
}
