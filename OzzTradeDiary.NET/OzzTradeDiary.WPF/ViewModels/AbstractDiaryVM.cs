using System.Collections.ObjectModel;
using TD.AppInfra.ViewModels;
using TD.Models;
using TD.RepositoryContracts;
using TD.SQLite;
using TD.WPF.Models;

namespace TD.WPF.ViewModels
{
    internal class AbstractDiaryVM : AbstractDataErrorInfoVM, ISymbolCreationContext
    {
        public AbstractDiaryVM()
        {
            var appSettings = AppSettings.GetAppSettings();
            var databasePath = appSettings.DatabasePath;

            CurrencyRepository = new CurrencyRepository(databasePath);
            SymbolRepository = new SymbolRepository(databasePath, currencyRepository: CurrencyRepository);
            ExchangeRepository = new ExchangeRepository(databasePath, CurrencyRepository, SymbolRepository);
            TradingAccountRepository = new TradingAccountRepository(databasePath, ExchangeRepository);

            Currencies = new ObservableCollection<Currency>();
            Exchanges = new ObservableCollection<Exchange>();
            TradingAccounts = new ObservableCollection<TradingAccount>();
            Symbols = new ObservableCollection<Symbol>();
        }

        public ICurrencyRepository CurrencyRepository { get; }
        public IExchangeRepository ExchangeRepository { get; }
        public ISymbolRepository SymbolRepository { get; }
        public ITradingAccountRepository TradingAccountRepository { get; }

        public ObservableCollection<Currency> Currencies { get; }
        public ObservableCollection<Exchange> Exchanges { get; }
        public ObservableCollection<TradingAccount> TradingAccounts { get; }
        public ObservableCollection<Symbol> Symbols { get; }

        public bool LoadCurrenciesInProgress { get; private set; } = false;
        public virtual async Task LoadCurrenciesAsync()
        {
            if (LoadCurrenciesInProgress)
                return;

            LoadCurrenciesInProgress = true;
            try
            {
                var items = await CurrencyRepository.GetAllAsync();
                ReplaceCollection(Currencies, items);
            }
            finally
            {
                LoadCurrenciesInProgress = false;
            }

            OnCurrenciesLoaded();
        }
        protected virtual void OnCurrenciesLoaded() { /* For override in derived classes */ }

        public bool LoadExchangesInProgress { get; private set; } = false;
        public virtual async Task LoadExchangesAsync()
        {
            if (LoadExchangesInProgress)
                return;

            LoadExchangesInProgress = true;
            try
            {
                var items = await ExchangeRepository.GetAllAsync();
                ReplaceCollection(Exchanges, items);
            }
            finally
            {
                LoadExchangesInProgress = false;
            }

            OnExchangesLoaded();
        }
        protected virtual void OnExchangesLoaded() { /* For override in derived classes */ }

        public bool LoadTradingAccountsInProgress { get; private set; } = false;
        public virtual async Task LoadTradingAccountsAsync()
        {
            if (LoadTradingAccountsInProgress)
                return;

            LoadTradingAccountsInProgress = true;
            try
            {
                var items = await TradingAccountRepository.GetAllAsync();
                ReplaceCollection(TradingAccounts, items);
            }
            finally
            {
                LoadTradingAccountsInProgress = false;
            }

            OnTradingAccountsLoaded();
        }
        protected virtual void OnTradingAccountsLoaded() { /* For override in derived classes */ }

        public bool LoadSymbolsInProgress { get; private set; } = false;
        public virtual async Task LoadSymbolsAsync()
        {
            if (LoadSymbolsInProgress)
                return;

            LoadSymbolsInProgress = true;
            try
            {
                var items = await SymbolRepository.GetAllAsync();
                ReplaceCollection(Symbols, items);
            }
            finally
            {
                LoadSymbolsInProgress = false;
            }

            OnSymbolsLoaded();
        }
        protected virtual void OnSymbolsLoaded() { /* For override in derived classes */ }

        public async Task SaveCurrencyAsync(Currency currency)
        {
            if (currency.Id <= 0)
                currency.Id = await CurrencyRepository.CreateAsync(currency);
            else
                await CurrencyRepository.UpdateAsync(currency);
        }

        public async Task SaveExchangeAsync(Exchange exchange)
        {
            if (exchange.Id <= 0)
                exchange.Id = await ExchangeRepository.CreateAsync(exchange);
            else
                await ExchangeRepository.UpdateAsync(exchange);
        }

        public async Task SaveTradingAccountAsync(TradingAccount tradingAccount)
        {
            if (tradingAccount.Id <= 0)
                tradingAccount.Id = await TradingAccountRepository.CreateAsync(tradingAccount);
            else
                await TradingAccountRepository.UpdateAsync(tradingAccount);
        }

        public async Task SaveSymbolAsync(Symbol symbol)
        {
            if (symbol.Id <= 0)
                symbol.Id = await SymbolRepository.CreateAsync(symbol);
            else
                await SymbolRepository.UpdateAsync(symbol);
        }


        public Currency? SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                if (_selectedCurrency?.Id == value?.Id)
                    return;
                _selectedCurrency = value;
                RaisePropertyChanged(nameof(SelectedCurrency));
            }
        }
        Currency? _selectedCurrency = null;

        public Exchange? SelectedExchange
        {
            get => _selectedExchange;
            set
            {
                if (_selectedExchange?.Id == value?.Id)
                    return;
                _selectedExchange = value;
                RaisePropertyChanged(nameof(SelectedExchange));
            }
        }
        Exchange? _selectedExchange = null;

        public TradingAccount? SelectedTradingAccount
        {
            get => _selectedTradingAccount;
            set
            {
                if (_selectedTradingAccount?.Id == value?.Id)
                    return;
                _selectedTradingAccount = value;
                RaisePropertyChanged(nameof(SelectedTradingAccount));
            }
        }
        TradingAccount? _selectedTradingAccount = null;

        public Symbol? SelectedSymbol
        {
            get => _selectedSymbol;
            set
            {
                if (_selectedSymbol?.Id == value?.Id)
                    return;
                _selectedSymbol = value;
                RaisePropertyChanged(nameof(SelectedSymbol));
            }
        }
        Symbol? _selectedSymbol = null;


        protected static void ReplaceCollection<T>(ObservableCollection<T> target, IEnumerable<T> source)
        {
            target.Clear();
            foreach (var item in source)
            {
                target.Add(item);
            }
        }
    }
}
