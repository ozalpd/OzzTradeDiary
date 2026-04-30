using System.Windows;
using TD.AppInfra.Services;
using TD.i18n;
using TD.Models;
using TD.WPF.Services;
using TD.WPF.ViewModels;

namespace TD.WPF.Commands.Maintenance;

internal class TradingAccountCreateCommand : AbstractCommand
{
    private readonly AbstractDiaryVM _viewModel;
    private readonly IWindowDialogService _windowDialogService;
    private readonly IExchangeLookupService _exchangeLookupService;

    public TradingAccountCreateCommand(AbstractDiaryVM viewModel, IWindowDialogService windowDialogService,
        IExchangeLookupService exchangeLookupService)
    {
        _viewModel = viewModel;
        _windowDialogService = windowDialogService;
        _exchangeLookupService = exchangeLookupService;
    }

    public override async void Execute(object? parameter)
    {
        if (parameter is not Window owner)
            return;

        try
        {
            var dialogResult = _windowDialogService.ShowTradingAccountCreateDialog(owner, _exchangeLookupService, PreselectedExchange);

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
        finally
        {
            PreselectedExchange = null;
        }
    }

    /// <summary>
    /// Before executing the command, we can set this property to have the exchange preselected in the trading account creation dialog.
    /// This is useful when you want to create a trading account for a specific exchange and want to save the user from having to select it manually.
    /// </summary>
    public Exchange? PreselectedExchange { get; set; } = null;
}
