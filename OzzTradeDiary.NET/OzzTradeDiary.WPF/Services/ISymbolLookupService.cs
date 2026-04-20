using TD.Models;

namespace TD.WPF.Services;

internal interface ISymbolLookupService
{
    Task<IReadOnlyList<Symbol>> GetActiveSymbolsAsync();
}
