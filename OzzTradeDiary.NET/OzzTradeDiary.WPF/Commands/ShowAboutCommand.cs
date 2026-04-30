using System.Windows;
using TD.AppInfra.Commands;
using TD.WPF.Dialogs;

namespace TD.WPF.Commands;

internal class ShowAboutCommand : AbstractCommand
{
    public override void Execute(object? parameter)
    {
        var aboutDialog = new AboutDialog
        {
            Owner = Application.Current.MainWindow
        };
        aboutDialog.ShowDialog();
    }
}
