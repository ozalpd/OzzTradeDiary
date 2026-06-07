using System.Windows;
using TD.AppInfra.Commands;
using TD.i18n;
using TD.Models;
using TD.WPF.Services;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Commands.Trades
{
    public partial class TradeImageCreateCommand : AbstractCommand
    {
        protected readonly TradeHistoryVM _viewModel;
        private readonly IWindowDialogService _windowDialogService;

        public TradeImageCreateCommand(TradeHistoryVM viewModel, IWindowDialogService windowDialogService)
        {
            _viewModel = viewModel;
            _windowDialogService = windowDialogService;
        }

        public override bool CanExecute(object? parameter)
        {
            return PreselectedTrade != null;
        }

        public override async void Execute(object? parameter)
        {
            if (parameter is not Window owner)
                return;

            try
            {
                var dialogResult = _windowDialogService.ShowTradeImageCreateDialog(owner, PreselectedTrade);

                if (dialogResult.IsConfirmed && dialogResult.TradeImage is not null)
                {
                    var TradeImage = dialogResult.TradeImage;
                    await _viewModel.SaveTradeImageAsync(TradeImage);
                    _viewModel.SelectedTradeImage = _viewModel.TradeImages.FirstOrDefault(x => x.Id == TradeImage.Id);
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
        /// Before executing the command, we can set this property to have the trade preselected in the TradeImage creation dialog.
        /// This is useful when you want to create a TradeImage for a specific trade and want to save the user from having to select it manually.
        /// </summary>
        public Trade? PreselectedTrade { get; set; } = null;
    }
}