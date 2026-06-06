using System.Windows;
using TD.AppInfra.Commands;
using TD.WPF.Services;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Commands.Trades
{
    public partial class TradeImageDetailCommand : AbstractCommand
    {
        protected readonly TradeHistoryVM _viewModel;
        private readonly IWindowDialogService _windowDialogService;

        public TradeImageDetailCommand(TradeHistoryVM viewModel, IWindowDialogService windowDialogService)
        {
            _viewModel = viewModel;
            _windowDialogService = windowDialogService;
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedTradeImage is not null;
        }

        public override async void Execute(object? parameter)
        {
            if (_viewModel.SelectedTradeImage is null)
                return;

            if (parameter is not Window owner)
                return;

            var tradeImage = _viewModel.SelectedTradeImage;
            _windowDialogService.ShowTradeImageDetailDialog(owner, tradeImage);
        }

    }
}