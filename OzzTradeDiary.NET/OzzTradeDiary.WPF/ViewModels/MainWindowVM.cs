using System.Collections.ObjectModel;
using TD.Models;
using TD.SQLite;
using TD.WPF.Commands;
using TD.WPF.Models;

namespace TD.WPF.ViewModels
{
    internal class MainWindowVM : AbstractDiaryVM
    {
        public MainWindowVM()
        {
            var appSettings = AppSettings.GetAppSettings();
            var databasePath = appSettings.DatabasePath;

            ShowMaintenanceCommand = new ShowMaintenanceCommand();
            ShowAboutCommand = new ShowAboutCommand();
        }

        public ShowMaintenanceCommand ShowMaintenanceCommand { get; }
        public ShowAboutCommand ShowAboutCommand { get; }
    }
}
