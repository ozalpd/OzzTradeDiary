using System.Windows;
using TD.WPF.Services;
using TD.WPF.ViewModels;

namespace TD.WPF.Commands;

internal class EditTradingAccountCommand : AbstractCommand
{
    private readonly AbstractDiaryVM _viewModel;
    private readonly IWindowDialogService _windowDialogService;

    public EditTradingAccountCommand(AbstractDiaryVM viewModel, IWindowDialogService windowDialogService)
    {
        _viewModel = viewModel;
        _windowDialogService = windowDialogService;
    }

    public override bool CanExecute(object? parameter)
    {
        return _viewModel.SelectedTradingAccount is not null;
    }

    public override async void Execute(object? parameter)
    {
        if (_viewModel.SelectedTradingAccount is null)
            return;

        if (parameter is not Window owner)
            return;

        int id = _viewModel.SelectedTradingAccount.Id;

        try
        {
            var dialogResult = _windowDialogService.ShowTradingAccountEditDialog(owner, _viewModel.SelectedTradingAccount);
            if (dialogResult.IsConfirmed && dialogResult.IsDirty)
            {
                await _viewModel.SaveTradingAccountsAsync();
                await _viewModel.LoadTradingAccountsAsync();
            }
            else if (dialogResult.IsDirty)
            {
                await _viewModel.LoadCurrenciesAsync();
            }

            _viewModel.SelectedTradingAccount = _viewModel.TradingAccounts.FirstOrDefault(x => x.Id == id);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Edit trading account failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
