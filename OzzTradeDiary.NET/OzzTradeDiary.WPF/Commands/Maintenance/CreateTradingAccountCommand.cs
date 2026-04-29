using System.Windows;
using TD.AppInfra.Services;
using TD.i18n;
using TD.Models;
using TD.WPF.Services;
using TD.WPF.ViewModels;

namespace TD.WPF.Commands.Maintenance;

internal class CreateTradingAccountCommand : AbstractCommand
{
    private readonly AbstractDiaryVM _viewModel;
    private readonly IWindowDialogService _windowDialogService;
    private readonly IExchangeLookupService _exchangeLookupService;
    private readonly Exchange? _preselectedExchange;

    public CreateTradingAccountCommand(AbstractDiaryVM viewModel, IWindowDialogService windowDialogService,
        IExchangeLookupService exchangeLookupService, Exchange? preselectedExchange = null)
    {
        _viewModel = viewModel;
        _windowDialogService = windowDialogService;
        _exchangeLookupService = exchangeLookupService;
        _preselectedExchange = preselectedExchange;
    }

    public override async void Execute(object? parameter)
    {
        if (parameter is not Window owner)
            return;

        try
        {
            var dialogResult = _windowDialogService.ShowTradingAccountCreateDialog(owner, _exchangeLookupService, _preselectedExchange);

            if (dialogResult.IsConfirmed && dialogResult.TradingAccount is not null)
            {
                var tradingAccount = dialogResult.TradingAccount;
                await _viewModel.SaveTradingAccountAsync(tradingAccount);
                await _viewModel.LoadTradingAccountsAsync();

                _viewModel.SelectedTradingAccount = _viewModel.TradingAccounts.FirstOrDefault(x => x.Id == tradingAccount.Id);
            }
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
