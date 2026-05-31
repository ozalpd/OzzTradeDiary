using System.Collections.ObjectModel;
using TD.Models;
using TD.RepositoryContracts;
using TD.WPF.Commands.Trades;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeHistoryVM
    {
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
        /// Removes <paramref name="slOrder"/> from <see cref="Trade.StopLossOrders"/> of
        /// <see cref="SelectedTrade"/>, then persists <em>all</em> remaining stop-loss orders
        /// in a single repository call (<see cref="ITradeRepository.SaveStopLossOrdersAsync"/>).
        /// </summary>
        /// <remarks>
        /// The save call persists the entire <see cref="Trade.StopLossOrders"/> collection after
        /// removal, so any other in-memory modifications to that collection are also committed.
        /// Returns <c>false</c> without saving if <see cref="SelectedTrade"/> is <c>null</c> or
        /// <paramref name="slOrder"/> has not been persisted yet (<c>Id == 0</c>).
        /// </remarks>
        /// <param name="slOrder">The stop-loss order to delete.</param>
        /// <returns><c>true</c> if the order was found and removed; otherwise <c>false</c>.</returns>
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

        private void ReplaceStopLossOrders()
        {
            StopLossOrders.Clear();
            if (SelectedTrade != null)
            {
                var direction = SelectedTrade.TradeDirection;
                var sortedOrders = direction == TradeDirection.Short
                                 ? SelectedTrade.StopLossOrders.OrderBy(o => o.OrderPrice)
                                 : SelectedTrade.StopLossOrders.OrderByDescending(o => o.OrderPrice);
                ReplaceCollection(StopLossOrders, sortedOrders);
            }
            RaiseStopLossOrderCmdCanExecute();
        }

        private void RaiseStopLossOrderCmdCanExecute()
        {
            StopLossOrderCreateCommand.RaiseCanExecuteChanged();
            StopLossOrderDeleteCommand.RaiseCanExecuteChanged();
            StopLossOrderEditCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Adds or updates <paramref name="slOrder"/> within <see cref="Trade.StopLossOrders"/> of
        /// <see cref="SelectedTrade"/>, then persists <em>all</em> stop-loss orders of that trade
        /// in a single repository call (<see cref="ITradeRepository.SaveStopLossOrdersAsync"/>).
        /// </summary>
        /// <remarks>
        /// Despite its singular name, this method always saves the entire
        /// <see cref="Trade.StopLossOrders"/> collection — not just the supplied order.
        /// If <paramref name="slOrder"/> is new (<c>Id == 0</c>) it is appended to the collection
        /// before saving; if it already exists it is updated in place.
        /// </remarks>
        /// <param name="slOrder">The stop-loss order to add or update.</param>
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
