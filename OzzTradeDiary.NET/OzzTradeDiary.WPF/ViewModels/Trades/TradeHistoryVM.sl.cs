using System.Collections.ObjectModel;
using TD.Extensions;
using TD.Models;
using TD.RepositoryContracts;
using TD.WPF.Commands.Trades;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeHistoryVM
    {
        public IStopLossOrderRepository StopLossOrderRepository { get; }
        public ObservableCollection<StopLossOrder> StopLossOrders { get; }
        public StopLossOrderCreateCommand StopLossOrderCreateCommand { get; }
        public StopLossOrderDeleteCommand StopLossOrderDeleteCommand { get; }
        public StopLossOrderEditCommand StopLossOrderEditCommand { get; }

        public StopLossOrder? SelectedStopLossOrder
        {
            get { return _selectedStopLossOrder; }
            set
            {
                _selectedStopLossOrder = value;
                StopLossOrderEditCommand.RaiseCanExecuteChanged();
                StopLossOrderDeleteCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(nameof(SelectedStopLossOrder));
            }
        }
        StopLossOrder? _selectedStopLossOrder;

        /// <summary>
        /// Gets the collection of available ExitOrderType enum members for selection or display.
        /// </summary>
        public IEnumerable<EnumValueItem<ExitOrderType>> ExitOrderTypeValues { get; }

        public async Task LoadStopLossOrdersAsync()
        {
            if (SelectedTrade != null)
                await LoadNavigationCollectionsAsync(SelectedTrade);
            ReplaceStopLossOrders();
        }

        private void ReplaceStopLossOrders()
        {
            StopLossOrders.Clear();
            if (SelectedTrade != null)
                ReplaceCollection(StopLossOrders, SelectedTrade.StopLossOrders);

            RaiseStopLossOrderCmdCanExecute();
        }

        private void RaiseStopLossOrderCmdCanExecute()
        {
            StopLossOrderCreateCommand.RaiseCanExecuteChanged();
            StopLossOrderDeleteCommand.RaiseCanExecuteChanged();
            StopLossOrderEditCommand.RaiseCanExecuteChanged();
        }

        public async Task SaveStopLossOrderAsync(StopLossOrder stopLossOrder)
        {
            if (stopLossOrder.Id <= 0)
                stopLossOrder.Id = await StopLossOrderRepository.CreateAsync(stopLossOrder);
            else
                await StopLossOrderRepository.UpdateAsync(stopLossOrder);
        }
    }
}
