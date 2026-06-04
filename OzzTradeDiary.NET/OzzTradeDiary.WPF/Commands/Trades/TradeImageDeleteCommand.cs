using System.Windows;
using TD.AppInfra.Commands;
using TD.i18n;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Commands.Trades
{
    public partial class TradeImageDeleteCommand : AbstractCommand
    {
        protected readonly TradeHistoryVM _viewModel;

        public TradeImageDeleteCommand(TradeHistoryVM viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            var tradeImage = _viewModel.SelectedTradeImage;
            if (tradeImage is null)
                return false;

            if (tradeImage.Id < 1)
                return false;

            return true;
        }

        public override async void Execute(object? parameter)
        {
            var TradeImage = _viewModel.SelectedTradeImage;
            if (TradeImage is null)
                return;

            if (TradeImage.Id < 1)
                return;

            var result = MessageBox.Show(
                MessageStrings.AreYouSureToDelete,
                CommonStrings.AppTitle,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            await _viewModel.DeleteTradeImageAsync(TradeImage);
        }
    }
}