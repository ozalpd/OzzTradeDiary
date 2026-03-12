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
            ExitCommand = new ExitCommand();
        }

        public ShowMaintenanceCommand ShowMaintenanceCommand { get; }
        public ShowAboutCommand ShowAboutCommand { get; }
        public ExitCommand ExitCommand { get; }
    }
}
