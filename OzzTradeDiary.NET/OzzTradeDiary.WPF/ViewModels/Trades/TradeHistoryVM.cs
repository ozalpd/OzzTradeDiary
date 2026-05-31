using System.Collections.ObjectModel;
using System.ComponentModel;
using TD.AppInfra.Services;
using TD.Models;
using TD.RepositoryContracts;
using TD.WPF.Commands.Trades;
using TD.WPF.Services;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeHistoryVM : TradeListVM
    {
        public TradeHistoryVM(ITradeRepository tradeRepository, IWindowDialogService windowDialogService,
                              ISymbolLookupService symbolLookupService, ITradingAccountLookupService tradingAccountLookupService)
            : base(tradeRepository, windowDialogService, symbolLookupService, tradingAccountLookupService)
        {
            _allSymbols = new List<Symbol>();
            _symbolLookup = symbolLookupService;
            _tradingAccountLookup = tradingAccountLookupService;

            EntryOrders = new ObservableCollection<EntryOrder>();
            EntryOrderCreateCommand = new EntryOrderCreateCommand(this, windowDialogService);
            EntryOrderDeleteCommand = new EntryOrderDeleteCommand(this);
            EntryOrderEditCommand = new EntryOrderEditCommand(this, windowDialogService);

            StopLossOrders = new ObservableCollection<StopLossOrder>();
            StopLossOrderCreateCommand = new StopLossOrderCreateCommand(this, windowDialogService);
            StopLossOrderDeleteCommand = new StopLossOrderDeleteCommand(this);
            StopLossOrderEditCommand = new StopLossOrderEditCommand(this, windowDialogService);

            TakeProfitOrders = new ObservableCollection<TakeProfitOrder>();
            TakeProfitOrderCreateCommand = new TakeProfitOrderCreateCommand(this, windowDialogService);
            TakeProfitOrderDeleteCommand = new TakeProfitOrderDeleteCommand(this);
            TakeProfitOrderEditCommand = new TakeProfitOrderEditCommand(this, windowDialogService);

            QueryVM.PageSize = 50;
        }


        protected override void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            if (e.PropertyName == nameof(SelectedTrade))
            {
                ReplaceEntryOrders();
                ReplaceStopLossOrders();
                ReplaceTakeProfitOrders();

                EntryOrderCreateCommand.PreselectedTrade = SelectedTrade;
                TakeProfitOrderCreateCommand.PreselectedTrade = SelectedTrade;
                StopLossOrderCreateCommand.PreselectedTrade = SelectedTrade;
                RaiseEntryOrderCmdCanExecute();
                RaiseStopLossOrderCmdCanExecute();
                RaiseTakeProfitOrderCmdCanExecute();
            }
        }

        /// <summary>
        /// Forces WPF bindings to refresh trade's derived properties.
        /// Necessary because <see cref="Trade"/> does not implement INotifyPropertyChanged.
        /// </summary>
        public void RefreshTrades()
        {
            if (Trades == null || Trades.Count == 0)
                return;

            var trade = SelectedTrade;
            var trades = Trades.ToList();
            ReplaceCollection(Trades, trades);

            if (trade == null)
                return;

            SelectedTrade = null;
            SelectedTrade = trade;
        }


        private IReadOnlyList<Symbol> _allSymbols;
        private ISymbolLookupService _symbolLookup { get; }
        private ITradingAccountLookupService _tradingAccountLookup { get; }

        public ObservableCollection<Symbol> Symbols { get; } = new();

        public TradingAccount? FilterTradingAccount
        {
            get => _byTradingAccount;
            set
            {
                _byTradingAccount = value;
                if (_byTradingAccount == null)
                {
                    ReplaceCollection(Symbols, _allSymbols);
                    QueryVM.ByTradingAccountId = null;
                }
                else
                {
                    int exchangeId = _byTradingAccount.ExchangeId;
                    var symbols = _allSymbols.Where(s => s.ExchangeId == exchangeId)
                                             .ToList();
                    ReplaceCollection(Symbols, symbols);
                    QueryVM.ByTradingAccountId = _byTradingAccount.Id;
                }
                RaisePropertyChanged(nameof(FilterTradingAccount));
            }
        }
        private TradingAccount? _byTradingAccount;
        public ObservableCollection<TradingAccount> TradingAccounts { get; } = new();

        public async Task InitializeAsync()
        {
            _allSymbols = await _symbolLookup.GetSymbolsAsync(isActive: true);
            ReplaceCollection(Symbols, _allSymbols);

            var accounts = await _tradingAccountLookup.GetTradingAccountsAsync(isActive: true);
            ReplaceCollection(TradingAccounts, accounts);
        }
    }
}
