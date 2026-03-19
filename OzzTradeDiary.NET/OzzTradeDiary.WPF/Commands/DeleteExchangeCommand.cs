using System.Windows;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Commands;

internal class DeleteExchangeCommand : AbstractCommand
{
    private readonly MaintenanceWindowVM _viewModel;

    public DeleteExchangeCommand(MaintenanceWindowVM viewModel)
    {
        _viewModel = viewModel;
    }

    public override bool CanExecute(object? parameter)
    {
        return _viewModel.CanDeleteSelectedExchange();
    }

    public override void Execute(object? parameter)
    {
        var result = MessageBox.Show(
            "Are you sure to delete?",
            "Ozz Trade Diary",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes)
            return;

        _viewModel.DeleteSelectedExchange();
    }
}
