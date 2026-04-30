using System.Windows;
using TD.AppInfra.Commands;

namespace TD.WPF.Commands;

internal class ExitCommand : AbstractCommand
{
    public override void Execute(object? parameter)
    {
        Application.Current.Shutdown();
    }
}
