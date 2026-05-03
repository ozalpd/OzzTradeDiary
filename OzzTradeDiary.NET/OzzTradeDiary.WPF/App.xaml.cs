using System.Globalization;
using System.Windows;
using TD.SQLite;
using TD.WPF.Models;
using TD.WPF.Views;

namespace TD.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var settings = AppSettings.GetAppSettings();

            if (!string.IsNullOrWhiteSpace(settings.UiCulture))
            {
                var culture = new CultureInfo(settings.UiCulture);
                Thread.CurrentThread.CurrentUICulture = culture;
                Thread.CurrentThread.CurrentCulture = culture;
            }
            string databasePath = settings.DatabasePath;
            var currencyRepository = new CurrencyRepository(databasePath);
            var exchangeRepository = new ExchangeRepository(databasePath, currencyRepository: currencyRepository);
            var tradeRepository = new TradeRepository(databasePath,
                                  entryOrderRepository: new EntryOrderRepository(databasePath),
                                  stopLossOrderRepository: new StopLossOrderRepository(databasePath),
                                  symbolRepository: new SymbolRepository(databasePath, exchangeRepository: exchangeRepository, currencyRepository: currencyRepository),
                                  takeProfitOrderRepository: new TakeProfitOrderRepository(databasePath),
                                  tradeImageRepository: new TradeImageRepository(databasePath),
                                  tradingAccountRepository: new TradingAccountRepository(databasePath, exchangeRepository: exchangeRepository));
            new MainWindow(tradeRepository).Show();
        }
    }
}
