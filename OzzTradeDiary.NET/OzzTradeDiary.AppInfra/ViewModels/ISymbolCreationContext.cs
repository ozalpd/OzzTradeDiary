using System.Collections.ObjectModel;
using TD.Models;
using TD.RepositoryContracts;

namespace TD.AppInfra.ViewModels
{
    public interface ISymbolCreationContext
    {
        ObservableCollection<Symbol> Symbols { get; }
        Symbol? SelectedSymbol { get; set; }
        Task SaveSymbolAsync(Symbol symbol);
        Task LoadSymbolsAsync();
        ISymbolRepository SymbolRepository { get; }
    }
}
