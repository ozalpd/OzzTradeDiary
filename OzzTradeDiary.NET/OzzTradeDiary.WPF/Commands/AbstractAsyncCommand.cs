using System.Windows.Input;

namespace TD.WPF.Commands;

internal interface IAsyncCommand : ICommand
{
    Task ExecuteAsync(object? parameter);
}

internal abstract class AbstractAsyncCommand : AbstractCommand, IAsyncCommand
{
    private bool _isExecuting;

    public sealed override bool CanExecute(object? parameter)
    {
        return !_isExecuting && CanExecuteAsync(parameter);
    }

    protected virtual bool CanExecuteAsync(object? parameter)
    {
        return true;
    }

    public sealed override async void Execute(object? parameter)
    {
        if (!CanExecute(parameter))
            return;

        try
        {
            _isExecuting = true;
            RaiseCanExecuteChanged();
            await ExecuteAsync(parameter);
        }
        finally
        {
            _isExecuting = false;
            RaiseCanExecuteChanged();
        }
    }

    public abstract Task ExecuteAsync(object? parameter);
}
