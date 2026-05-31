using System.Windows;
using TD.AppInfra.Commands;
using TD.i18n;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Commands.Trades
{
    public partial class StopLossOrderDeleteCommand : AbstractCommand
    {
        protected readonly TradeHistoryVM _viewModel;

        public StopLossOrderDeleteCommand(TradeHistoryVM viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            var stopLossOrder = _viewModel.SelectedStopLossOrder;
            if (stopLossOrder is null)
                return false;

            if (stopLossOrder.Id < 1)
                return false;


            var result = _viewModel.StopLossOrderRepository
                                   .CanDeleteAsync(stopLossOrder.Id)
                                   .GetAwaiter()
                                   .GetResult();
            return result;
        }

        public override async void Execute(object? parameter)
        {
            var slOrder = _viewModel.SelectedStopLossOrder;
            if (slOrder is null)
                return;

            if (slOrder.Id < 1)
                return;

            var result = MessageBox.Show(
                MessageStrings.AreYouSureToDelete,
                CommonStrings.AppTitle,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            await _viewModel.DeleteStopLossOrderAsync(slOrder);
        }
    }
}