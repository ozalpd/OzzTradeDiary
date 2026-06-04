using System.Windows;
using TD.AppInfra.Commands;
using TD.i18n;
using TD.WPF.Services;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Commands.Trades
{
    public partial class TradeImageEditCommand : AbstractCommand
    {
        protected readonly TradeHistoryVM _viewModel;
        private readonly IWindowDialogService _windowDialogService;

        public TradeImageEditCommand(TradeHistoryVM viewModel, IWindowDialogService windowDialogService)
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

            var TradeImage = _viewModel.SelectedTradeImage;

            try
            {
                var dialogResult = _windowDialogService.ShowTradeImageEditDialog(owner, _viewModel.SelectedTradeImage);
                if (dialogResult.IsConfirmed && dialogResult.IsDirty)
                {
                    await _viewModel.SaveTradeImageAsync(TradeImage);
                }
                else if (dialogResult.IsDirty)
                {
                    _viewModel.RefreshTrades();
                }

                _viewModel.SelectedTradeImage = _viewModel.TradeImages.FirstOrDefault(x => x.Id == TradeImage.Id);
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