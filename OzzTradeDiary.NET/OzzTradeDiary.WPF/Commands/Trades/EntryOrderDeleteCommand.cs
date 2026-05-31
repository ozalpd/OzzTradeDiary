using System.Windows;
using TD.AppInfra.Commands;
using TD.i18n;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Commands.Trades
{
    public partial class EntryOrderDeleteCommand : AbstractCommand
    {
        protected readonly TradeHistoryVM _viewModel;

        public EntryOrderDeleteCommand(TradeHistoryVM viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            var entryOrder = _viewModel.SelectedEntryOrder;
            if (entryOrder is null)
                return false;

            if (entryOrder.Id < 1)
                return false;


            var result = _viewModel.EntryOrderRepository
                                   .CanDeleteAsync(entryOrder.Id)
                                   .GetAwaiter()
                                   .GetResult();
            return result;
        }

        public override async void Execute(object? parameter)
        {
            var entryOrder = _viewModel.SelectedEntryOrder;
            if (entryOrder is null)
                return;

            if (entryOrder.Id < 1)
                return;

            var result = MessageBox.Show(
                MessageStrings.AreYouSureToDelete,
                CommonStrings.AppTitle,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            await _viewModel.DeleteEntryOrderAsync(entryOrder);
        }
    }
}