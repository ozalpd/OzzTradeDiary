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
            var symbolRepository = new SymbolRepository(databasePath, currencyRepository: currencyRepository);
            var exchangeRepository = new ExchangeRepository(databasePath,
                                     currencyRepository: currencyRepository,
                                     symbolRepository: symbolRepository);
            var tradingAccountRepository = new TradingAccountRepository(databasePath, exchangeRepository: exchangeRepository);
            var tradeRepository = new TradeRepository(databasePath,
                                  entryOrderRepository: new EntryOrderRepository(databasePath),
                                  stopLossOrderRepository: new StopLossOrderRepository(databasePath),
                                  symbolRepository: symbolRepository,
                                  takeProfitOrderRepository: new TakeProfitOrderRepository(databasePath),
                                  tradeImageRepository: new TradeImageRepository(databasePath),
                                  tradingAccountRepository: tradingAccountRepository);

            new MainWindow(tradeRepository,
                           currencyRepository, exchangeRepository,
                           symbolRepository, tradingAccountRepository).Show();
        }
    }
}

