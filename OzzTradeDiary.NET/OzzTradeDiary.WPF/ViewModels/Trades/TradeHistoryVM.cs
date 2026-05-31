using System.Collections.ObjectModel;
using System.ComponentModel;
using TD.AppInfra.Services;
using TD.Models;
using TD.RepositoryContracts;
using TD.WPF.Commands.Trades;
using TD.WPF.Services;
using static TD.Extensions.EnumExtension;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeHistoryVM : TradeListVM
    {
        public TradeHistoryVM(IEntryOrderRepository entryOrderRepository, ITakeProfitOrderRepository takeProfitOrderRepository,
                              ITradeRepository tradeRepository, IStopLossOrderRepository stopLossOrderRepository,
                              IWindowDialogService windowDialogService, ISymbolLookupService symbolLookupService,
                              ITradingAccountLookupService tradingAccountLookupService)
            : base(tradeRepository, windowDialogService, symbolLookupService, tradingAccountLookupService)
        {
            _allSymbols = new List<Symbol>();
            _symbolLookup = symbolLookupService;
            _tradingAccountLookup = tradingAccountLookupService;

            EntryOrderTypeValues = GetValues<EntryOrderType>();
            ExitOrderTypeValues = GetValues<ExitOrderType>();
            ExitOrderForTpValues = ExitOrderTypeValues.Where(v => v.Value <= ExitOrderType.TrailingStop)
                                                      .ToList();

            EntryOrderRepository = entryOrderRepository;
            EntryOrders = new ObservableCollection<EntryOrder>();
            EntryOrderCreateCommand = new EntryOrderCreateCommand(this, windowDialogService);
            EntryOrderDeleteCommand = new EntryOrderDeleteCommand(this);
            EntryOrderEditCommand = new EntryOrderEditCommand(this, windowDialogService);

            StopLossOrderRepository = stopLossOrderRepository;
            StopLossOrders = new ObservableCollection<StopLossOrder>();
            StopLossOrderCreateCommand = new StopLossOrderCreateCommand(this, windowDialogService);
            StopLossOrderDeleteCommand = new StopLossOrderDeleteCommand(this);
            StopLossOrderEditCommand = new StopLossOrderEditCommand(this, windowDialogService);

            TakeProfitOrderRepository = takeProfitOrderRepository;
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
