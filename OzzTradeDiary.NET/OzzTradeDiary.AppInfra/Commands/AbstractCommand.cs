using System.Windows.Input;

namespace TD.AppInfra.Commands;

public abstract class AbstractCommand : ICommand
{
    public abstract void Execute(object? parameter);

    public virtual bool CanExecute(object? parameter)
    {
        return true;
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? CanExecuteChanged;
}