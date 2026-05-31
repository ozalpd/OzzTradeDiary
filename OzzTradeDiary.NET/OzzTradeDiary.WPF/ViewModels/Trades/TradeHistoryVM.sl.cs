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

        public async Task<bool> DeleteStopLossOrderAsync(StopLossOrder slOrder)
        {
            if (SelectedTrade == null)
                return false;

            ArgumentNullException.ThrowIfNull(slOrder, nameof(slOrder));
            bool isDeleted = false;
            if (slOrder.Id > 0)
            {
                isDeleted = SelectedTrade.StopLossOrders.Remove(slOrder);
                await TradeRepository.SaveStopLossOrdersAsync(SelectedTrade);
                RefreshTrades();
            }

            return isDeleted;
        }

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

        public async Task SaveStopLossOrderAsync(StopLossOrder slOrder)
        {
            if (SelectedTrade == null)
                return;
            if (slOrder.Id == 0)
            {
                SelectedTrade.StopLossOrders.Add(slOrder);
                slOrder.Trade = SelectedTrade;
            }
            await TradeRepository.SaveStopLossOrdersAsync(SelectedTrade);
            RefreshTrades();
        }
    }
}
