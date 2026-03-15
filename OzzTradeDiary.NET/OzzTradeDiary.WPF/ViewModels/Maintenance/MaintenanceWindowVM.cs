using TD.WPF.Models;

namespace TD.WPF.ViewModels.Maintenance
{
    internal class MaintenanceWindowVM : AbstractDiaryVM
    {
        public MaintenanceWindowVM()
        {
            var appSettings = AppSettings.GetAppSettings();
            var databasePath = appSettings.DatabasePath;

        }

        public async Task LoadAllAsync()
        {
            await LoadCurrenciesAsync();
            await LoadExchangesAsync();
            await LoadTradingAccountsAsync();
            await LoadSymbolsAsync();
        }
    }
}
