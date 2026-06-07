using System.Globalization;
using System.Windows;
using TD.AppInfra.Models;
using TD.SQLite;
using TD.WPF.Models;
using TD.WPF.Services;
using TD.WPF.Views;

namespace TD.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Timers.Timer? _backupTimer;

        private async void OnStartupAsync(object sender, StartupEventArgs e)
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
            var entryOrderRepository = new EntryOrderRepository(databasePath);
            var stopLossOrderRepository = new StopLossOrderRepository(databasePath);
            var takeProfitOrderRepository = new TakeProfitOrderRepository(databasePath);
            var tradingAccountRepository = new TradingAccountRepository(databasePath, exchangeRepository);
            var tradeRepository = new TradeRepository(databasePath,
                                  entryOrderRepository: entryOrderRepository,
                                  stopLossOrderRepository: stopLossOrderRepository,
                                  symbolRepository: symbolRepository,
                                  takeProfitOrderRepository: takeProfitOrderRepository,
                                  tradeImageRepository: new TradeImageRepository(databasePath),
                                  tradingAccountRepository: tradingAccountRepository);

            var dataSources = new AppDataSources(currencyRepository, entryOrderRepository, exchangeRepository,
                                                 stopLossOrderRepository, symbolRepository, takeProfitOrderRepository,
                                                 tradingAccountRepository, tradeRepository);
            if (settings.AutoBackupEnabled)
            {
                await AutoBackupHelper.RunAsync();
                int backupInterval = (int)settings.AutoBackupIntervalMinutes;
                _backupTimer = new System.Timers.Timer(backupInterval * 60 * 1000);
                _backupTimer.Elapsed += async (sender, args) => await AutoBackupHelper.RunAsync();
                _backupTimer.Start();
            }

            new MainWindow(dataSources).Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            _backupTimer?.Stop();
            _backupTimer?.Dispose();

            var settings = AppSettings.GetAppSettings();
            if (settings.AutoBackupEnabled)
                Task.Run(() => AutoBackupHelper.RunAsync(byPassIsBackupDue: true)).GetAwaiter()
                                                                                  .GetResult();

            settings.Save();
        }
    }
}

