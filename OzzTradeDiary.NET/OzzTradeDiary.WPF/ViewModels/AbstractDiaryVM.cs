using System.Collections.ObjectModel;
using TD.AppInfra.ViewModels;
using TD.Models;
using TD.SQLite;
using TD.WPF.Models;

namespace TD.WPF.ViewModels
{
    internal class AbstractDiaryVM : AbstractDataErrorInfoVM
    {
        public AbstractDiaryVM()
        {
            var appSettings = AppSettings.GetAppSettings();
            var databasePath = appSettings.DatabasePath;

            CurrencyRepository = new CurrencyRepository(databasePath);
            SymbolRepository = new SymbolRepository(databasePath);
            TradingAccountRepository = new TradingAccountRepository(databasePath);
            ExchangeRepository = new ExchangeRepository(databasePath, SymbolRepository, TradingAccountRepository);

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

        public virtual async Task LoadCurrenciesAsync()
        {
            var items = await CurrencyRepository.GetAllAsync();
            ReplaceCollection(Currencies, items);
        }

        public virtual async Task LoadExchangesAsync()
        {
            var items = await ExchangeRepository.GetAllAsync();
            ReplaceCollection(Exchanges, items);
        }

        public virtual async Task LoadTradingAccountsAsync()
        {
            var items = await TradingAccountRepository.GetAllAsync();
            ReplaceCollection(TradingAccounts, items);
        }

        public virtual async Task LoadSymbolsAsync()
        {
            var items = await SymbolRepository.GetAllAsync();
            ReplaceCollection(Symbols, items);
        }

        public async Task SaveCurrenciesAsync()
        {
            foreach (var item in Currencies)
            {
                if (item.Id <= 0)
                    item.Id = await CurrencyRepository.CreateAsync(item);
                else
                    await CurrencyRepository.UpdateAsync(item);
            }
        }

        public async Task SaveExchangesAsync()
        {
            foreach (var item in Exchanges)
            {
                if (item.Id <= 0)
                    item.Id = await ExchangeRepository.CreateAsync(item);
                else
                    await ExchangeRepository.UpdateAsync(item);
            }
        }

        public async Task SaveTradingAccountsAsync()
        {
            foreach (var item in TradingAccounts)
            {
                if (item.Id <= 0)
                    item.Id = await TradingAccountRepository.CreateAsync(item);
                else
                    await TradingAccountRepository.UpdateAsync(item);
            }
        }

        public async Task SaveSymbolsAsync()
        {
            foreach (var item in Symbols)
            {
                if (item.Id <= 0)
                    item.Id = await SymbolRepository.CreateAsync(item);
                else
                    await SymbolRepository.UpdateAsync(item);
            }
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
