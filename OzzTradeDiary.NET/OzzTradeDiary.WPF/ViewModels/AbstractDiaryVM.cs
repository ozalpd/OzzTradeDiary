using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TD.Models;
using TD.SQLite;
using TD.WPF.Models;

namespace TD.WPF.ViewModels
{
    internal class AbstractDiaryVM
    {
        public AbstractDiaryVM()
        {
            var appSettings = AppSettings.GetAppSettings();
            var databasePath = appSettings.DatabasePath;


            MetadataRepository = new SqliteDatabaseMetadataRepository(databasePath);
            CurrencyRepository = new SqliteDatabaseCurrencyRepository(databasePath, MetadataRepository);
            ExchangeRepository = new SqliteDatabaseExchangeRepository(databasePath, MetadataRepository);
            SymbolRepository = new SqliteDatabaseSymbolRepository(databasePath, MetadataRepository);
            TradingAccountRepository = new SqliteDatabaseTradingAccountRepository(databasePath, MetadataRepository);

            Currencies = new ObservableCollection<Currency>();
            Exchanges = new ObservableCollection<Exchange>();
            TradingAccounts = new ObservableCollection<TradingAccount>();
            Symbols = new ObservableCollection<Symbol>();
        }

        public SqliteDatabaseMetadataRepository MetadataRepository { get; }
        public IDatabaseCurrencyRepository CurrencyRepository { get; }
        public IDatabaseExchangeRepository ExchangeRepository { get; }
        public IDatabaseSymbolRepository SymbolRepository { get; }
        public IDatabaseTradingAccountRepository TradingAccountRepository { get; }

        public ObservableCollection<Currency> Currencies { get; }
        public ObservableCollection<Exchange> Exchanges { get; }
        public ObservableCollection<TradingAccount> TradingAccounts { get; }
        public ObservableCollection<Symbol> Symbols { get; }

        public async Task LoadCurrenciesAsync()
        {
            var items = await CurrencyRepository.GetAllAsync();
            ReplaceCollection(Currencies, items);
        }

        public async Task LoadExchangesAsync()
        {
            var items = await ExchangeRepository.GetAllAsync();
            ReplaceCollection(Exchanges, items);
        }

        public async Task LoadTradingAccountsAsync()
        {
            var items = await TradingAccountRepository.GetAllAsync();
            ReplaceCollection(TradingAccounts, items);
        }

        public async Task LoadSymbolsAsync()
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

        private static void ReplaceCollection<T>(ObservableCollection<T> target, IEnumerable<T> source)
        {
            target.Clear();
            foreach (var item in source)
            {
                target.Add(item);
            }
        }
    }
}
