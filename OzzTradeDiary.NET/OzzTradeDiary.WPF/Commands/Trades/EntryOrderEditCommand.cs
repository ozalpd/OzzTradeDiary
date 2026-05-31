using System.Windows;
using TD.AppInfra.Commands;
using TD.i18n;
using TD.WPF.Services;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Commands.Trades
{
    public partial class EntryOrderEditCommand : AbstractCommand
    {
        protected readonly TradeHistoryVM _viewModel;
        private readonly IWindowDialogService _windowDialogService;

        public EntryOrderEditCommand(TradeHistoryVM viewModel, IWindowDialogService windowDialogService)
        {
            _viewModel = viewModel;
            _windowDialogService = windowDialogService;
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedEntryOrder != null && _viewModel.SelectedEntryOrder.Id > 0
                   && _viewModel.SelectedEntryOrder.Trade != null
                   && _viewModel.SelectedEntryOrder.Trade.IsActiveOrWaiting;
        }

        public override async void Execute(object? parameter)
        {
            if (_viewModel.SelectedEntryOrder is null)
                return;

            if (parameter is not Window owner)
                return;

            var entryOrder = _viewModel.SelectedEntryOrder;

            try
            {
                var dialogResult = _windowDialogService.ShowEntryOrderEditDialog(owner, _viewModel.SelectedEntryOrder);
                if (dialogResult.IsConfirmed && dialogResult.IsDirty)
                {
                    await _viewModel.SaveEntryOrderAsync(entryOrder);
                }
                else if (dialogResult.IsDirty)
                {
                    _viewModel.RefreshTrades();
                }

                _viewModel.SelectedEntryOrder = _viewModel.EntryOrders.FirstOrDefault(x => x.Id == entryOrder.Id);
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