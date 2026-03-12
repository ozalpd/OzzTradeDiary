using System.Windows;
using TD.WPF.Views;

namespace TD.WPF.Commands;

internal class ShowMaintenanceCommand : AbstractCommand
{
    private MaintenanceWindow? _maintenanceWindow;

    public override void Execute(object? parameter)
    {
        if (_maintenanceWindow == null || !_maintenanceWindow.IsLoaded)
        {
            _maintenanceWindow = new MaintenanceWindow();
            _maintenanceWindow.Closed += (s, e) => _maintenanceWindow = null;
            _maintenanceWindow.Show();
        }
        else
        {
            if (_maintenanceWindow.WindowState == WindowState.Minimized)
            {
                _maintenanceWindow.WindowState = WindowState.Normal;
            }
            _maintenanceWindow.Activate();
            _maintenanceWindow.Focus();
        }
    }
}
