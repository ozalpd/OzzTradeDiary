using System.Windows;
using TD.AppInfra.Services;
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
                _viewModel.TradingAccounts.Add(tradingAccount);
                await _viewModel.SaveTradingAccountsAsync();
                await _viewModel.LoadTradingAccountsAsync();

                _viewModel.SelectedTradingAccount = _viewModel.TradingAccounts.FirstOrDefault(x => x.Id == tradingAccount.Id);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Create trading account failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
