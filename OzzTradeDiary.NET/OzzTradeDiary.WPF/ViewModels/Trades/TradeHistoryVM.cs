using System.Collections.ObjectModel;
using System.ComponentModel;
using TD.AppInfra.Services;
using TD.Extensions;
using TD.Models;
using TD.RepositoryContracts;
using TD.WPF.Commands.Trades;
using TD.WPF.Services;
using static TD.Extensions.EnumExtension;

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

            _allTradeDateTypeValues = GetOrderedValues<TradeDateType>();
            ReplaceCollection(TradeDateTypeValues, _allTradeDateTypeValues);
            QueryVM.ByTradeDateType = TradeDateType.UpdateTime;
            TradeStatusQueryValues = GetOrderedValues<TradeStatusQuery>();

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
            QueryVM.ByTradeStatus = 0;
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

            if (e.PropertyName == nameof(QueryVM.ByTradeStatus))
            {
                var tradeStatus = QueryVM.ByTradeStatus;
                if (tradeStatus == null || tradeStatus == TradeStatusQuery.All)
                {
                    ReplaceCollection(TradeDateTypeValues, _allTradeDateTypeValues);
                    QueryVM.ByTradeDateType = TradeDateType.UpdateTime;
                }
                else if (tradeStatus == TradeStatusQuery.Active || tradeStatus == TradeStatusQuery.ActiveOrWaiting)
                {
                    var tradeDateTypes = _allTradeDateTypeValues.Where(v => v.Value != TradeDateType.ExitTime
                                                                         && v.Value != TradeDateType.CancellationTime)
                                                                .ToList();
                    ReplaceCollection(TradeDateTypeValues, tradeDateTypes);

                    if (tradeStatus == TradeStatusQuery.Active)
                        QueryVM.ByTradeDateType = TradeDateType.EntryTime;

                    if (tradeStatus == TradeStatusQuery.ActiveOrWaiting)
                        QueryVM.ByTradeDateType = TradeDateType.UpdateTime;
                }
                else if (tradeStatus == TradeStatusQuery.Closed)
                {
                    var tradeDateTypes = _allTradeDateTypeValues.Where(v => v.Value != TradeDateType.CancellationTime)
                                                                .ToList();
                    ReplaceCollection(TradeDateTypeValues, tradeDateTypes);
                    QueryVM.ByTradeDateType = TradeDateType.ExitTime;
                }
                else if (tradeStatus == TradeStatusQuery.Cancelled)
                {
                    var tradeDateTypes = _allTradeDateTypeValues.Where(v => v.Value == TradeDateType.UpdateTime
                                                                         || v.Value == TradeDateType.CancellationTime)
                                                                .ToList();
                    ReplaceCollection(TradeDateTypeValues, tradeDateTypes);
                    QueryVM.ByTradeDateType = TradeDateType.CancellationTime;
                }
                else //This must be Planned, Pending, Missed or MissedOrCancelled
                {
                    var tradeDateTypes = _allTradeDateTypeValues.Where(v => v.Value == TradeDateType.UpdateTime)
                                                                .ToList();
                    ReplaceCollection(TradeDateTypeValues, tradeDateTypes);
                    QueryVM.ByTradeDateType = TradeDateType.UpdateTime;
                }
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

        public TradeDateType? FilterTradeDateType
        {
            get => QueryVM.ByTradeDateType;
            set
            {
                QueryVM.ByTradeDateType = value;
                RaisePropertyChanged(nameof(FilterTradeDateType));
            }
        }
        private TradeDateType? _byTradeDateType;

        private IEnumerable<EnumValueItem<TradeDateType>> _allTradeDateTypeValues;
        public ObservableCollection<EnumValueItem<TradeDateType>> TradeDateTypeValues { get; private set; } = new ObservableCollection<EnumValueItem<TradeDateType>>();

        public TradeStatusQuery? FilterTradeStatus
        {
            get => QueryVM.ByTradeStatus;
            set
            {
                QueryVM.ByTradeStatus = value;
                RaisePropertyChanged(nameof(FilterTradeStatus));
                RaisePropertyChanged(nameof(FilterTradeDateType));
            }
        }

        public IEnumerable<EnumValueItem<TradeStatusQuery>> TradeStatusQueryValues { get; private set; } = Array.Empty<EnumValueItem<TradeStatusQuery>>();

        public async Task InitializeAsync()
        {
            _allSymbols = await _symbolLookup.GetSymbolsAsync(isActive: true);
            ReplaceCollection(Symbols, _allSymbols);

            var accounts = await _tradingAccountLookup.GetTradingAccountsAsync(isActive: true);
            ReplaceCollection(TradingAccounts, accounts);
        }
    }
}
