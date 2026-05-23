using System.Collections.ObjectModel;
using TD.AppInfra.Services;
using TD.Models;
using TD.RepositoryContracts;
using TD.WPF.Services;

namespace TD.WPF.ViewModels.Trades
{
    public class TradeHistoryVM : TradeListVM
    {
        public TradeHistoryVM(ITradeRepository tradeRepository, IWindowDialogService windowDialogService,
                              ISymbolLookupService symbolLookupService, ITradingAccountLookupService tradingAccountLookupService)
            : base(tradeRepository, windowDialogService, symbolLookupService, tradingAccountLookupService)
        {
            _allSymbols = new List<Symbol>();
            _symbolLookup = symbolLookupService;
            _tradingAccountLookup = tradingAccountLookupService;

            QueryVM.PageSize = 50;
        }

        private IReadOnlyList<Symbol> _allSymbols;
        private ISymbolLookupService _symbolLookup { get; }
        private ITradingAccountLookupService _tradingAccountLookup { get; }

        public ObservableCollection<Symbol> Symbols { get; } = new();
        public ObservableCollection<TradingAccount> TradingAccounts { get; } = new();

        public TradingAccount? SelectedTradingAccount
        {
            get => _selectedTradingAccount;
            set
            {
                _selectedTradingAccount = value;
                if (_selectedTradingAccount == null)
                {
                    ReplaceCollection(Symbols, _allSymbols);
                    QueryVM.ByTradingAccountId = null;
                }
                else
                {
                    int exchangeId = _selectedTradingAccount.ExchangeId;
                    var symbols = _allSymbols.Where(s => s.ExchangeId == exchangeId)
                                             .ToList();
                    ReplaceCollection(Symbols, symbols);
                    QueryVM.ByTradingAccountId = _selectedTradingAccount.Id;
                }
                RaisePropertyChanged(nameof(SelectedTradingAccount));
            }
        }
        private TradingAccount? _selectedTradingAccount;

        public async Task InitializeAsync()
        {
            _allSymbols = await _symbolLookup.GetSymbolsAsync(isActive: true);
            ReplaceCollection(Symbols, _allSymbols);

            var accounts = await _tradingAccountLookup.GetTradingAccountsAsync(isActive: true);
            ReplaceCollection(TradingAccounts, accounts);
        }
    }
}
