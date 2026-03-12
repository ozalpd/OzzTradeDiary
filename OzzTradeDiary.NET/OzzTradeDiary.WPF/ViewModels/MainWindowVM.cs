using TD.SQLite;
using TD.WPF.Models;

namespace TD.WPF.ViewModels
{
    internal class MainWindowVM : AbstractViewModel
    {
        public MainWindowVM()
        {
            var appSettings = AppSettings.GetAppSettings();
            var databasePath = appSettings.DatabasePath;

            MetadataRepository = new SqliteDatabaseMetadataRepository(databasePath);
            CurrencyRepository = new SqliteDatabaseCurrencyRepository(databasePath, MetadataRepository);
            ExchangeRepository = new SqliteDatabaseExchangeRepository(databasePath, MetadataRepository);
            TradingAccountRepository = new SqliteDatabaseTradingAccountRepository(databasePath, MetadataRepository);    
        }

        public SqliteDatabaseMetadataRepository MetadataRepository { get; }
        public IDatabaseCurrencyRepository CurrencyRepository { get; }
        public IDatabaseExchangeRepository ExchangeRepository { get; }
        public IDatabaseTradingAccountRepository TradingAccountRepository { get; }
    }
}
