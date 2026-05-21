using System.Windows;
using TD.AppInfra.Commands;
using TD.AppInfra.Models;
using TD.WPF.Views.Maintenance;

namespace TD.WPF.Commands;

internal class ShowMaintenanceCommand : AbstractCommand
{
    private readonly AppDataSources _dataSources;
    private MaintenanceWindow? _maintenanceWindow;

    public ShowMaintenanceCommand(AppDataSources dataSources)
    {
        _dataSources = dataSources;
    }

    public override void Execute(object? parameter)
    {
        if (_maintenanceWindow == null || !_maintenanceWindow.IsLoaded)
        {
            _maintenanceWindow = new MaintenanceWindow(_dataSources);
            _maintenanceWindow.Closed += (s, e) => _maintenanceWindow = null;
            _maintenanceWindow.Show();
        }
        else
        {
            if (_maintenanceWindow.WindowState == WindowState.Minimized)
                _maintenanceWindow.WindowState = WindowState.Normal;
            _maintenanceWindow.Activate();
            _maintenanceWindow.Focus();
        }
    }
}
