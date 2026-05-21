using System.Collections.ObjectModel;
using TD.AppInfra.Services;
using TD.Helpers;
using TD.Models;
using TD.RepositoryContracts;
using TD.WPF.Commands.Trades;
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
            TradesLoadCommand = new TradesLoadCommand(this);
        }

        private IReadOnlyList<Symbol> _allSymbols;
        private ISymbolLookupService _symbolLookup { get; }
        private ITradingAccountLookupService _tradingAccountLookup { get; }
        public TradesLoadCommand TradesLoadCommand { get; }

        public ObservableCollection<Symbol> Symbols { get; } = new();
        public ObservableCollection<TradingAccount> TradingAccounts { get; } = new();


        public int? FilterTradingAccountId
        {
            get => QueryParams.TradingAccountId;
            set
            {
                QueryParams.TradingAccountId = value;
                RaisePropertyChanged(nameof(FilterTradingAccountId));
            }
        }

        public int? FilterSymbolId
        {
            get => QueryParams.SymbolId;
            set
            {
                QueryParams.SymbolId = value;
                RaisePropertyChanged(nameof(FilterSymbolId));
            }
        }

        public DateTime? EntryTimeMin
        {
            get => QueryParams.EntryTimeMin;
            set
            {
                QueryParams.EntryTimeMin = value;
                RaisePropertyChanged(nameof(EntryTimeMin));
            }
        }

        public DateTime? EntryTimeMax
        {
            get => QueryParams.EntryTimeMax;
            set
            {
                QueryParams.EntryTimeMax = value;
                RaisePropertyChanged(nameof(EntryTimeMax));
            }
        }

        public TradingAccount? SelectedTradingAccount
        {
            get => _selectedTradingAccount;
            set
            {
                _selectedTradingAccount = value;
                if (_selectedTradingAccount == null)
                {
                    ReplaceCollection(Symbols, _allSymbols);
                    FilterTradingAccountId = null;
                }
                else
                {
                    int exchangeId = _selectedTradingAccount.ExchangeId;
                    var symbols = _allSymbols.Where(s => s.ExchangeId == exchangeId)
                                             .ToList();
                    ReplaceCollection(Symbols, symbols);
                    FilterTradingAccountId = _selectedTradingAccount.Id;
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

        public async Task LoadTradesWithFilterAsync()
        {
            await LoadTradesAsync();
        }
    }
}
