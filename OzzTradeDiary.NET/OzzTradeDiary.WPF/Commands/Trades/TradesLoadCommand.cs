using TD.AppInfra.Commands;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Commands.Trades
{
    public class TradesLoadCommand : AbstractCommand
    {
        private readonly TradeHistoryVM _viewModel;

        public TradesLoadCommand(TradeHistoryVM viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return !_viewModel.LoadTradesInProgress;
        }

        public override async void Execute(object? parameter)
        {
            RaiseCanExecuteChanged();
            await _viewModel.LoadTradesWithFilterAsync();
            RaiseCanExecuteChanged();
        }
    }
}
