using System.Windows;
using TD.AppInfra.Commands;
using TD.i18n;
using TD.Models;
using TD.WPF.Services;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Commands.Trades
{
    public partial class TakeProfitOrderCreateCommand : AbstractCommand
    {
        protected readonly TradeHistoryVM _viewModel;
        private readonly IWindowDialogService _windowDialogService;

        public TakeProfitOrderCreateCommand(TradeHistoryVM viewModel, IWindowDialogService windowDialogService)
        {
            _viewModel = viewModel;
            _windowDialogService = windowDialogService;
        }

        public override bool CanExecute(object? parameter)
        {
            return PreselectedTrade != null && PreselectedTrade.IsActiveOrWaiting;
        }

        public override async void Execute(object? parameter)
        {
            if (parameter is not Window owner)
                return;

            try
            {
                var dialogResult = _windowDialogService.ShowTakeProfitOrderCreateDialog(owner, PreselectedTrade);

                if (dialogResult.IsConfirmed && dialogResult.TakeProfitOrder is not null)
                {
                    var takeProfitOrder = dialogResult.TakeProfitOrder;
                    await _viewModel.SaveTakeProfitOrderAsync(takeProfitOrder);
                    _viewModel.SelectedTakeProfitOrder = _viewModel.TakeProfitOrders.FirstOrDefault(x => x.Id == takeProfitOrder.Id);
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

        /// <summary>
        /// Before executing the command, we can set this property to have the trade preselected in the TakeProfitOrder creation dialog.
        /// This is useful when you want to create a TakeProfitOrder for a specific trade and want to save the user from having to select it manually.
        /// </summary>
        public Trade? PreselectedTrade { get; set; } = null;
    }
}