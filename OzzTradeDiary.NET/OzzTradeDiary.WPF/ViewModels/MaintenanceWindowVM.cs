using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TD.Models;
using TD.WPF.Models;

namespace TD.WPF.ViewModels
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
