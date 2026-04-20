using TD.Models;
using TD.SQLite;

namespace TD.WPF.Services;

internal class SymbolLookupService : ISymbolLookupService
{
    private readonly ISymbolRepository _symbolRepository;

    public SymbolLookupService(ISymbolRepository symbolRepository)
    {
        _symbolRepository = symbolRepository;
    }

    public async Task<IReadOnlyList<Symbol>> GetActiveSymbolsAsync()
    {
        var symbols = await _symbolRepository.GetAllAsync(isActive: true);
        return symbols.ToList();
    }
}
