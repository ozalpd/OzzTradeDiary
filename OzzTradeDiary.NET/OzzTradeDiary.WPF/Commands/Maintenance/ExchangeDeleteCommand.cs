using System.Windows;
using TD.AppInfra.Commands;
using TD.i18n;
using TD.WPF.ViewModels;

namespace TD.WPF.Commands.Maintenance;

internal class ExchangeDeleteCommand : AbstractAsyncCommand
{
    private readonly AbstractDiaryVM _viewModel;

    public ExchangeDeleteCommand(AbstractDiaryVM viewModel)
    {
        _viewModel = viewModel;
    }

    protected override bool CanExecuteAsync(object? parameter)
    {
        if (_viewModel.SelectedExchange is null)
            return false;

        if (_viewModel.LoadSymbolsInProgress || _viewModel.LoadTradingAccountsInProgress)
            return false;

        return !_viewModel.Symbols.Any(s => s.ExchangeId == _viewModel.SelectedExchange.Id)
            && !_viewModel.TradingAccounts.Any(a => a.ExchangeId == _viewModel.SelectedExchange.Id);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        var exchangeToDelete = _viewModel.SelectedExchange;
        if (exchangeToDelete is null || exchangeToDelete.Id <= 0)
            return;

        var result = MessageBox.Show(
            MessageStrings.AreYouSureToDelete,
            CommonStrings.AppTitle,
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes)
            return;

        bool deleted = await _viewModel.ExchangeRepository.DeleteAsync(exchangeToDelete.Id);
        if (!deleted)
            return;

        _viewModel.SelectedExchange = null;
        _viewModel.Exchanges.Remove(exchangeToDelete);
    }
}
