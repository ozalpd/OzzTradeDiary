using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TD.Models;
using TD.WPF.Commands.Maintenance;
using TD.WPF.Services;

namespace TD.WPF.ViewModels.Maintenance
{
    internal class MaintenanceWindowVM : AbstractDiaryVM
    {
        public MaintenanceWindowVM(IWindowDialogService windowDialogService)
        {
            CreateCurrencyCommand = new CreateCurrencyCommand(this, windowDialogService);
            EditCurrencyCommand = new EditCurrencyCommand(this, windowDialogService);

            CreateExchangeCommand = new CreateExchangeCommand(this, windowDialogService);
            EditExchangeCommand = new EditExchangeCommand(this, windowDialogService);
            DeleteExchangeCommand = new DeleteExchangeCommand(this);

            CreateSymbolCommand = new CreateSymbolCommand(this, windowDialogService);
            EditSymbolCommand = new EditSymbolCommand(this, windowDialogService);

            CreateTradingAccountCommand = new CreateTradingAccountCommand(this, windowDialogService);
            EditTradingAccountCommand = new EditTradingAccountCommand(this, windowDialogService);

            SymbolExchanges = new ObservableCollection<Exchange>();
            FilteredSymbols = new ObservableCollection<Symbol>();

            PropertyChanged += OnPropertyChanged;
            Symbols.CollectionChanged += OnDependencyCollectionChanged;
            TradingAccounts.CollectionChanged += OnDependencyCollectionChanged;
        }

        public CreateCurrencyCommand CreateCurrencyCommand { get; }
        public EditCurrencyCommand EditCurrencyCommand { get; }

        public CreateExchangeCommand CreateExchangeCommand { get; }
        public EditExchangeCommand EditExchangeCommand { get; }
        public DeleteExchangeCommand DeleteExchangeCommand { get; }

        public CreateSymbolCommand CreateSymbolCommand { get; }
        public EditSymbolCommand EditSymbolCommand { get; }

        public CreateTradingAccountCommand CreateTradingAccountCommand { get; }
        public EditTradingAccountCommand EditTradingAccountCommand { get; }

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
            DeleteExchangeCommand.RaiseCanExecuteChanged();
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
                EditCurrencyCommand.RaiseCanExecuteChanged();

            if (e.PropertyName == nameof(SelectedExchange))
            {
                DeleteExchangeCommand.RaiseCanExecuteChanged();
                EditExchangeCommand.RaiseCanExecuteChanged();
            }

            if (e.PropertyName == nameof(SelectedExchange))
                DeleteExchangeCommand.RaiseCanExecuteChanged();

            if (e.PropertyName == nameof(SelectedSymbol))
                EditSymbolCommand.RaiseCanExecuteChanged();

            if (e.PropertyName == nameof(SelectedTradingAccount))
                EditTradingAccountCommand.RaiseCanExecuteChanged();
        }

        private void OnDependencyCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            EditCurrencyCommand.RaiseCanExecuteChanged();
            EditExchangeCommand.RaiseCanExecuteChanged();
            DeleteExchangeCommand.RaiseCanExecuteChanged();
            EditSymbolCommand.RaiseCanExecuteChanged();
            EditTradingAccountCommand.RaiseCanExecuteChanged();
        }
    }
}
