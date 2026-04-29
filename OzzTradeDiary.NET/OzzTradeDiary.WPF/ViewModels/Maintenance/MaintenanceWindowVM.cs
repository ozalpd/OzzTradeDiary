using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TD.Models;
using TD.WPF.Commands;
using TD.WPF.Services;

namespace TD.WPF.ViewModels.Maintenance
{
    internal class MaintenanceWindowVM : AbstractDiaryVM
    {
        private readonly DeleteExchangeCommand _deleteExchangeCommand;
        private readonly EditTradingAccountCommand _editTradingAccountCommand;

        public MaintenanceWindowVM(IWindowDialogService windowDialogService)
        {
            _deleteExchangeCommand = new DeleteExchangeCommand(this);
            _editTradingAccountCommand = new EditTradingAccountCommand(this, windowDialogService);
            DeleteExchangeCommand = _deleteExchangeCommand;
            CreateTradingAccountCommand = new CreateTradingAccountCommand(this, windowDialogService);
            EditTradingAccountCommand = _editTradingAccountCommand;
            SymbolExchanges = new ObservableCollection<Exchange>();
            FilteredSymbols = new ObservableCollection<Symbol>();

            PropertyChanged += OnPropertyChanged;
            Symbols.CollectionChanged += OnDependencyCollectionChanged;
            TradingAccounts.CollectionChanged += OnDependencyCollectionChanged;
        }

        public DeleteExchangeCommand DeleteExchangeCommand { get; }
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



        public bool CanDeleteSelectedExchange()
        {
            if (SelectedExchange is null)
                return false;

            var exchangeId = SelectedExchange.Id;
            return !Symbols.Any(symbol => symbol.ExchangeId == exchangeId)
                && !TradingAccounts.Any(account => account.ExchangeId == exchangeId);
        }

        public void DeleteSelectedExchange()
        {
            if (!CanDeleteSelectedExchange() || SelectedExchange is null)
                return;

            var exchangeToDelete = SelectedExchange;

            if (exchangeToDelete.Id > 0)
            {
                var deleted = ExchangeRepository.DeleteAsync(exchangeToDelete.Id).GetAwaiter().GetResult();
                if (!deleted)
                    return;
            }

            Exchanges.Remove(exchangeToDelete);
            SelectedExchange = null;
            _deleteExchangeCommand.RaiseCanExecuteChanged();
        }

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
            _deleteExchangeCommand.RaiseCanExecuteChanged();
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
            if (e.PropertyName == nameof(SelectedExchange))
                _deleteExchangeCommand.RaiseCanExecuteChanged();

            if (e.PropertyName == nameof(SelectedTradingAccount))
                _editTradingAccountCommand.RaiseCanExecuteChanged();
        }

        private void OnDependencyCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _deleteExchangeCommand.RaiseCanExecuteChanged();
            _editTradingAccountCommand.RaiseCanExecuteChanged();
        }
    }
}
