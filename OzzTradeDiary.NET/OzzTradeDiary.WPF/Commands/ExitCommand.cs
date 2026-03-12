using System.Windows;

namespace TD.WPF.Commands;

internal class ExitCommand : AbstractCommand
{
    public override void Execute(object? parameter)
    {
        Application.Current.Shutdown();
    }
}
