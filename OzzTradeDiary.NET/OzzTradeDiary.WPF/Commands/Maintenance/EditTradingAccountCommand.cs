using System.Windows;
using TD.i18n;
using TD.WPF.Services;
using TD.WPF.ViewModels;

namespace TD.WPF.Commands.Maintenance;

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

        var tradingAccount = _viewModel.SelectedTradingAccount;
        try
        {
            var dialogResult = _windowDialogService.ShowTradingAccountEditDialog(owner, _viewModel.SelectedTradingAccount);
            if (dialogResult.IsConfirmed && dialogResult.IsDirty)
            {
                await _viewModel.SaveTradingAccountAsync(tradingAccount);
                await _viewModel.LoadTradingAccountsAsync();
            }
            else if (dialogResult.IsDirty)
            {
                await _viewModel.LoadCurrenciesAsync();
            }

            _viewModel.SelectedTradingAccount = _viewModel.TradingAccounts.FirstOrDefault(x => x.Id == tradingAccount.Id);
        }
        catch (Exception ex)
        {
            string innerExceptionMsg = ex.InnerException != null ? $"\n{ex.InnerException.Message}" : "";
            MessageBox.Show($"{MessageStrings.SaveOperationFailed}\n{ex.Message}{innerExceptionMsg}",
                CommonStrings.AppTitle,
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
