using System.Windows;
using TD.AppInfra.Services;
using TD.i18n;
using TD.WPF.Services;
using TD.WPF.ViewModels;

namespace TD.WPF.Commands.Maintenance;

internal class CreateTradingAccountCommand : AbstractCommand
{
    private readonly AbstractDiaryVM _viewModel;
    private readonly IWindowDialogService _windowDialogService;

    public CreateTradingAccountCommand(AbstractDiaryVM viewModel, IWindowDialogService windowDialogService)
    {
        _viewModel = viewModel;
        _windowDialogService = windowDialogService;
    }

    public override async void Execute(object? parameter)
    {
        if (parameter is not Window owner)
            return;

        try
        {
            var exchangeLookupService = new ExchangeLookupService(_viewModel.ExchangeRepository);
            var dialogResult = _windowDialogService.ShowTradingAccountCreateDialog(owner, exchangeLookupService);

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
            MessageBox.Show(MessageStrings.SaveOperationFailed, CommonStrings.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
