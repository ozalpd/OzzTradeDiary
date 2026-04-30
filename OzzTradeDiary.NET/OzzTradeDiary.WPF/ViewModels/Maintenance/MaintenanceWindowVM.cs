using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TD.AppInfra.Services;
using TD.Models;
using TD.WPF.Commands.Maintenance;
using TD.WPF.Services;

namespace TD.WPF.ViewModels.Maintenance
{
    internal class MaintenanceWindowVM : AbstractDiaryVM
    {
        public MaintenanceWindowVM(IWindowDialogService windowDialogService)
        {
            var exchangeLookupService = new ExchangeLookupService(ExchangeRepository);
            var currencyLookupService = new CurrencyLookupService(CurrencyRepository);

            CurrencyCreateCommand = new CurrencyCreateCommand(this, windowDialogService);
            CurrencyEditCommand = new CurrencyEditCommand(this, windowDialogService);

            ExchangeCreateCommand = new ExchangeCreateCommand(this, windowDialogService);
            ExchangeEditCommand = new ExchangeEditCommand(this, windowDialogService);
            ExchangeDeleteCommand = new ExchangeDeleteCommand(this);

            SymbolCreateCommand = new SymbolCreateCommand(this, windowDialogService, exchangeLookupService, currencyLookupService);
            SymbolEditCommand = new SymbolEditCommand(this, windowDialogService);

            TradingAccountCreateCommand = new TradingAccountCreateCommand(this, windowDialogService, exchangeLookupService);
            TradingAccountEditCommand = new TradingAccountEditCommand(this, windowDialogService);

            SymbolExchanges = new ObservableCollection<Exchange>();
            FilteredSymbols = new ObservableCollection<Symbol>();

            PropertyChanged += OnPropertyChanged;
            Symbols.CollectionChanged += OnDependencyCollectionChanged;
            TradingAccounts.CollectionChanged += OnDependencyCollectionChanged;
        }

        public CurrencyCreateCommand CurrencyCreateCommand { get; }
        public CurrencyEditCommand CurrencyEditCommand { get; }

        public ExchangeCreateCommand ExchangeCreateCommand { get; }
        public ExchangeEditCommand ExchangeEditCommand { get; }
        public ExchangeDeleteCommand ExchangeDeleteCommand { get; }

        public SymbolCreateCommand SymbolCreateCommand { get; }
        public SymbolEditCommand SymbolEditCommand { get; }

        public TradingAccountCreateCommand TradingAccountCreateCommand { get; }
        public TradingAccountEditCommand TradingAccountEditCommand { get; }

        public ObservableCollection<Symbol> FilteredSymbols { get; }
        public ObservableCollection<Exchange> SymbolExchanges { get; }


        public Exchange? SelectedSymbolExchange
        {
            get => _selectedSymbolExchange;
            set
            {
                if (_selectedSymbolExchange?.Id == value?.Id)
                    return;
                _selectedSymbolExchange = value;
                RaisePropertyChanged(nameof(SelectedSymbolExchange));
                FilterSymbols();
            }
        }
        Exchange? _selectedSymbolExchange = null;

        public string SymbolSearchString
        {
            get => _symbolSearchString ?? string.Empty;
            set
            {
                _symbolSearchString = value;
                RaisePropertyChanged(nameof(SymbolSearchString));
                FilterSymbols();
            }
        }
        private string? _symbolSearchString;

        private void FilterSymbols()
        {
            if (SelectedSymbolExchange is null && string.IsNullOrWhiteSpace(SymbolSearchString))
            {
                ReplaceCollection(FilteredSymbols, Symbols);
                return;
            }

            int exchangeId = SelectedSymbolExchange?.Id ?? -1;
            IEnumerable<Symbol> filtered;
            if (exchangeId > 0)
            {
                filtered = Symbols.Where(symbol => symbol.ExchangeId == exchangeId);
            }
            else
            {
                filtered = Symbols;
            }

            if (!string.IsNullOrWhiteSpace(SymbolSearchString))
            {
                filtered = filtered.Where(symbol => symbol.TickerFull.Contains(SymbolSearchString, StringComparison.OrdinalIgnoreCase)
                         || (!string.IsNullOrEmpty(symbol.Description) && symbol.Description.Contains(SymbolSearchString, StringComparison.OrdinalIgnoreCase)));
            }
            ReplaceCollection(FilteredSymbols, filtered);
        }

        public async Task LoadAllAsync()
        {
            await LoadCurrenciesAsync();
            await LoadExchangesAsync();
            await LoadTradingAccountsAsync();
            await LoadSymbolsAsync();
            ExchangeDeleteCommand.RaiseCanExecuteChanged();
        }

        public override async Task LoadExchangesAsync()
        {
            await base.LoadExchangesAsync();
            var items = Exchanges.Where(exchange => exchange.HasAnySymbol).ToList();
            ReplaceCollection(SymbolExchanges, items);
        }

        public override async Task LoadSymbolsAsync()
        {
            await base.LoadSymbolsAsync();
            FilterSymbols();
        }

        private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedCurrency))
                CurrencyEditCommand.RaiseCanExecuteChanged();

            if (e.PropertyName == nameof(SelectedExchange))
            {
                ExchangeDeleteCommand.RaiseCanExecuteChanged();
                ExchangeEditCommand.RaiseCanExecuteChanged();
            }

            if (e.PropertyName == nameof(SelectedExchange))
                ExchangeDeleteCommand.RaiseCanExecuteChanged();
            if (e.PropertyName == nameof(SelectedSymbol))
                SymbolEditCommand.RaiseCanExecuteChanged();

            if (e.PropertyName == nameof(SelectedTradingAccount))
                TradingAccountEditCommand.RaiseCanExecuteChanged();
        }

        private void OnDependencyCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            CurrencyEditCommand.RaiseCanExecuteChanged();
            ExchangeEditCommand.RaiseCanExecuteChanged();
            ExchangeDeleteCommand.RaiseCanExecuteChanged();
            SymbolEditCommand.RaiseCanExecuteChanged();
            TradingAccountEditCommand.RaiseCanExecuteChanged();
        }
    }
}
