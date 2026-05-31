using System.Collections.ObjectModel;
using TD.Models;
using TD.RepositoryContracts;
using TD.WPF.Commands.Trades;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeHistoryVM
    {
        public ObservableCollection<EntryOrder> EntryOrders { get; }
        public EntryOrderCreateCommand EntryOrderCreateCommand { get; }
        public EntryOrderDeleteCommand EntryOrderDeleteCommand { get; }
        public EntryOrderEditCommand EntryOrderEditCommand { get; }

        public EntryOrder? SelectedEntryOrder
        {
            get { return _selectedEntryOrder; }
            set
            {
                _selectedEntryOrder = value;
                RaisePropertyChanged(nameof(SelectedEntryOrder));
                EntryOrderEditCommand.RaiseCanExecuteChanged();
                EntryOrderDeleteCommand.RaiseCanExecuteChanged();
            }
        }
        EntryOrder? _selectedEntryOrder;

        /// <summary>
        /// Removes <paramref name="entryOrder"/> from <see cref="Trade.EntryOrders"/> of
        /// <see cref="SelectedTrade"/>, then persists <em>all</em> remaining entry orders
        /// in a single repository call (<see cref="ITradeRepository.SaveEntryOrdersAsync"/>).
        /// </summary>
        /// <remarks>
        /// The save call persists the entire <see cref="Trade.EntryOrders"/> collection after
        /// removal, so any other in-memory modifications to that collection are also committed.
        /// Returns <c>false</c> without saving if <see cref="SelectedTrade"/> is <c>null</c> or
        /// <paramref name="entryOrder"/> has not been persisted yet (<c>Id == 0</c>).
        /// </remarks>
        /// <param name="entryOrder">The entry order to delete.</param>
        /// <returns><c>true</c> if the order was found and removed; otherwise <c>false</c>.</returns>
        public async Task<bool> DeleteEntryOrderAsync(EntryOrder entryOrder)
        {
            if (SelectedTrade == null)
                return false;

            ArgumentNullException.ThrowIfNull(entryOrder, nameof(entryOrder));
            bool isDeleted = false;
            if (entryOrder.Id > 0)
            {
                isDeleted = SelectedTrade.EntryOrders.Remove(entryOrder);
                await TradeRepository.SaveEntryOrdersAsync(SelectedTrade);
                RefreshTrades();
            }

            return isDeleted;
        }

        private void ReplaceEntryOrders()
        {
            EntryOrders.Clear();
            if (SelectedTrade != null)
            {
                var direction = SelectedTrade.TradeDirection;
                var sortedOrders = direction == TradeDirection.Long
                                 ? SelectedTrade.EntryOrders.OrderBy(o => o.OrderPrice)
                                 : SelectedTrade.EntryOrders.OrderByDescending(o => o.OrderPrice);
                ReplaceCollection(EntryOrders, sortedOrders);
            }
            RaiseEntryOrderCmdCanExecute();
        }

        private void RaiseEntryOrderCmdCanExecute()
        {
            EntryOrderCreateCommand.RaiseCanExecuteChanged();
            EntryOrderDeleteCommand.RaiseCanExecuteChanged();
            EntryOrderEditCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Adds or updates <paramref name="entryOrder"/> within <see cref="Trade.EntryOrders"/> of
        /// <see cref="SelectedTrade"/>, then persists <em>all</em> entry orders of that trade
        /// in a single repository call (<see cref="ITradeRepository.SaveEntryOrdersAsync"/>).
        /// </summary>
        /// <remarks>
        /// Despite its singular name, this method always saves the entire
        /// <see cref="Trade.EntryOrders"/> collection — not just the supplied order.
        /// If <paramref name="entryOrder"/> is new (<c>Id == 0</c>) it is appended to the collection
        /// before saving; if it already exists it is updated in place.
        /// </remarks>
        /// <param name="entryOrder">The entry order to add or update.</param>
        public async Task SaveEntryOrderAsync(EntryOrder entryOrder)
        {
            if (SelectedTrade == null)
                return;
            if (entryOrder.Id == 0)
            {
                SelectedTrade.EntryOrders.Add(entryOrder);
                entryOrder.Trade = SelectedTrade;
            }
            await TradeRepository.SaveEntryOrdersAsync(SelectedTrade);
            RefreshTrades();
        }
    }
}
