using System.Windows;
using TD.i18n;
using TD.WPF.Services;
using TD.WPF.ViewModels;

namespace TD.WPF.Commands.Maintenance
{
    internal class ExchangeCreateCommand : AbstractCommand
    {
        private readonly AbstractDiaryVM _viewModel;
        private readonly IWindowDialogService _windowDialogService;

        public ExchangeCreateCommand(AbstractDiaryVM viewModel, IWindowDialogService windowDialogService)
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
                var dialogResult = _windowDialogService.ShowExchangeCreateDialog(owner);

                if (dialogResult.IsConfirmed && dialogResult.Exchange is not null)
                {
                    var exchange = dialogResult.Exchange;
                    await _viewModel.SaveExchangeAsync(exchange);
                    await _viewModel.LoadExchangesAsync();

                    _viewModel.SelectedExchange = _viewModel.Exchanges.FirstOrDefault(x => x.Id == exchange.Id);
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
}
