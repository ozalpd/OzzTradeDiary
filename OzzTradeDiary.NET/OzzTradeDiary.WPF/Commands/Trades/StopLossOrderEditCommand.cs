using System.Windows;
using TD.AppInfra.Commands;
using TD.i18n;
using TD.WPF.Services;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Commands.Trades
{
    public partial class StopLossOrderEditCommand : AbstractCommand
    {
        protected readonly TradeHistoryVM _viewModel;
        private readonly IWindowDialogService _windowDialogService;

        public StopLossOrderEditCommand(TradeHistoryVM viewModel, IWindowDialogService windowDialogService)
        {
            _viewModel = viewModel;
            _windowDialogService = windowDialogService;
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedStopLossOrder is not null;
        }

        public override async void Execute(object? parameter)
        {
            if (_viewModel.SelectedStopLossOrder is null)
                return;

            if (parameter is not Window owner)
                return;

            var stopLossOrder = _viewModel.SelectedStopLossOrder;

            try
            {
                var dialogResult = _windowDialogService.ShowStopLossOrderEditDialog(owner, _viewModel.SelectedStopLossOrder);
                if (dialogResult.IsConfirmed && dialogResult.IsDirty)
                {
                    await _viewModel.SaveStopLossOrderAsync(stopLossOrder);
                }
                else if (dialogResult.IsDirty)
                {
                    _viewModel.RefreshTrades();
                }

                _viewModel.SelectedStopLossOrder = _viewModel.StopLossOrders.FirstOrDefault(x => x.Id == stopLossOrder.Id);
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