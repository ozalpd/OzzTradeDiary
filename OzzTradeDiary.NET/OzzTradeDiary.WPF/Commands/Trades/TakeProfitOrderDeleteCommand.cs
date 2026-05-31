using System.Windows;
using TD.AppInfra.Commands;
using TD.i18n;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Commands.Trades
{
    public partial class TakeProfitOrderDeleteCommand : AbstractCommand
    {
        protected readonly TradeHistoryVM _viewModel;

        public TakeProfitOrderDeleteCommand(TradeHistoryVM viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            var takeProfitOrder = _viewModel.SelectedTakeProfitOrder;
            if (takeProfitOrder is null)
                return false;

            if (takeProfitOrder.Id < 1)
                return false;


            var result = _viewModel.TakeProfitOrderRepository
                                   .CanDeleteAsync(takeProfitOrder.Id)
                                   .GetAwaiter()
                                   .GetResult();
            return result;
        }

        public override async void Execute(object? parameter)
        {
            var tpOrder = _viewModel.SelectedTakeProfitOrder;
            if (tpOrder is null)
                return;

            if (tpOrder.Id < 1)
                return;

            var result = MessageBox.Show(
                MessageStrings.AreYouSureToDelete,
                CommonStrings.AppTitle,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            await _viewModel.DeleteTakeProfitOrderAsync(tpOrder);
        }
    }
}