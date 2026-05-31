using System.Windows;
using TD.AppInfra.Commands;
using TD.i18n;
using TD.WPF.Services;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Commands.Trades
{
    public partial class TakeProfitOrderEditCommand : AbstractCommand
    {
        protected readonly TradeHistoryVM _viewModel;
        private readonly IWindowDialogService _windowDialogService;

        public TakeProfitOrderEditCommand(TradeHistoryVM viewModel, IWindowDialogService windowDialogService)
        {
            _viewModel = viewModel;
            _windowDialogService = windowDialogService;
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedTakeProfitOrder is not null;
        }

        public override async void Execute(object? parameter)
        {
            if (_viewModel.SelectedTakeProfitOrder is null)
                return;

            if (parameter is not Window owner)
                return;

            var takeProfitOrder = _viewModel.SelectedTakeProfitOrder;

            try
            {
                var dialogResult = _windowDialogService.ShowTakeProfitOrderEditDialog(owner, _viewModel.SelectedTakeProfitOrder);
                if (dialogResult.IsConfirmed && dialogResult.IsDirty)
                {
                    await _viewModel.SaveTakeProfitOrderAsync(takeProfitOrder);
                }
                else if (dialogResult.IsDirty)
                {
                    _viewModel.RefreshTrades();
                }

                _viewModel.SelectedTakeProfitOrder = _viewModel.TakeProfitOrders.FirstOrDefault(x => x.Id == takeProfitOrder.Id);
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