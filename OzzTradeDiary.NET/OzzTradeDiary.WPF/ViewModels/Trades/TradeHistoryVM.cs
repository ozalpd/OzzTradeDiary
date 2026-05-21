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
            SymbolLookup = symbolLookupService;
            TradingAccountLookup = tradingAccountLookupService;
            TradesLoadCommand = new TradesLoadCommand(this);
        }

        public ISymbolLookupService SymbolLookup { get; }
        public ITradingAccountLookupService TradingAccountLookup { get; }
        public TradesLoadCommand TradesLoadCommand { get; }

        public ObservableCollection<Symbol> Symbols { get; } = new();
        public ObservableCollection<TradingAccount> TradingAccounts { get; } = new();

        private int? _filterTradingAccountId;
        public int? FilterTradingAccountId
        {
            get => _filterTradingAccountId;
            set { _filterTradingAccountId = value; RaisePropertyChanged(nameof(FilterTradingAccountId)); }
        }

        private int? _filterSymbolId;
        public int? FilterSymbolId
        {
            get => _filterSymbolId;
            set { _filterSymbolId = value; RaisePropertyChanged(nameof(FilterSymbolId)); }
        }

        private DateTime? _entryTimeMin;
        public DateTime? EntryTimeMin
        {
            get => _entryTimeMin;
            set { _entryTimeMin = value; RaisePropertyChanged(nameof(EntryTimeMin)); }
        }

        private DateTime? _entryTimeMax;
        public DateTime? EntryTimeMax
        {
            get => _entryTimeMax;
            set { _entryTimeMax = value; RaisePropertyChanged(nameof(EntryTimeMax)); }
        }

        public async Task InitializeAsync()
        {
            var symbols = await SymbolLookup.GetSymbolsAsync(isActive: true);
            Symbols.Clear();
            foreach (var s in symbols)
                Symbols.Add(s);

            var accounts = await TradingAccountLookup.GetTradingAccountsAsync(isActive: true);
            TradingAccounts.Clear();
            foreach (var a in accounts)
                TradingAccounts.Add(a);
        }

        public async Task LoadTradesWithFilterAsync()
        {
            var qp = new TradeQueryParameters
            {
                TradingAccountId = FilterTradingAccountId,
                SymbolId = FilterSymbolId,
                EntryTimeMin = EntryTimeMin,
                EntryTimeMax = EntryTimeMax,
            };
            await LoadTradesAsync(qp);
        }
    }
}
