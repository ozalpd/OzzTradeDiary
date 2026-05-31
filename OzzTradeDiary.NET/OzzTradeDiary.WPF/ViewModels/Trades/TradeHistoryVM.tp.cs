using System.Collections.ObjectModel;
using TD.Extensions;
using TD.Models;
using TD.RepositoryContracts;
using TD.WPF.Commands.Trades;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeHistoryVM
    {
        public ObservableCollection<TakeProfitOrder> TakeProfitOrders { get; }
        public TakeProfitOrderCreateCommand TakeProfitOrderCreateCommand { get; }
        public TakeProfitOrderDeleteCommand TakeProfitOrderDeleteCommand { get; }
        public TakeProfitOrderEditCommand TakeProfitOrderEditCommand { get; }

        public TakeProfitOrder? SelectedTakeProfitOrder
        {
            get { return _selectedTakeProfitOrder; }
            set
            {
                _selectedTakeProfitOrder = value;
                TakeProfitOrderEditCommand.RaiseCanExecuteChanged();
                TakeProfitOrderDeleteCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(nameof(SelectedTakeProfitOrder));
            }
        }
        TakeProfitOrder? _selectedTakeProfitOrder;

        /// <summary>
        /// Gets the collection of available ExitOrderType enum members for selection or display.
        /// </summary>
        public IEnumerable<EnumValueItem<ExitOrderType>> ExitOrderForTpValues { get; }

        /// <summary>
        /// Removes <paramref name="tpOrder"/> from <see cref="Trade.TakeProfitOrders"/> of
        /// <see cref="SelectedTrade"/>, then persists <em>all</em> remaining take-profit orders
        /// in a single repository call (<see cref="ITradeRepository.SaveTakeProfitOrdersAsync"/>).
        /// </summary>
        /// <remarks>
        /// The save call persists the entire <see cref="Trade.TakeProfitOrders"/> collection after
        /// removal, so any other in-memory modifications to that collection are also committed.
        /// Returns <c>false</c> without saving if <see cref="SelectedTrade"/> is <c>null</c> or
        /// <paramref name="tpOrder"/> has not been persisted yet (<c>Id == 0</c>).
        /// </remarks>
        /// <param name="tpOrder">The take-profit order to delete.</param>
        /// <returns><c>true</c> if the order was found and removed; otherwise <c>false</c>.</returns>
        public async Task<bool> DeleteTakeProfitOrderAsync(TakeProfitOrder tpOrder)
        {
            if (SelectedTrade == null)
                return false;

            ArgumentNullException.ThrowIfNull(tpOrder, nameof(tpOrder));
            bool isDeleted = false;
            if (tpOrder.Id > 0)
            {
                isDeleted = SelectedTrade.TakeProfitOrders.Remove(tpOrder);
                await TradeRepository.SaveTakeProfitOrdersAsync(SelectedTrade);
                RefreshTrades();
            }

            return isDeleted;
        }

        private void ReplaceTakeProfitOrders()
        {
            TakeProfitOrders.Clear();
            if (SelectedTrade != null)
                ReplaceCollection(TakeProfitOrders, SelectedTrade.TakeProfitOrders);

            RaiseTakeProfitOrderCmdCanExecute();
        }

        private void RaiseTakeProfitOrderCmdCanExecute()
        {
            TakeProfitOrderCreateCommand.RaiseCanExecuteChanged();
            TakeProfitOrderDeleteCommand.RaiseCanExecuteChanged();
            TakeProfitOrderEditCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Adds or updates <paramref name="tpOrder"/> within <see cref="Trade.TakeProfitOrders"/> of
        /// <see cref="SelectedTrade"/>, then persists <em>all</em> take-profit orders of that trade
        /// in a single repository call (<see cref="ITradeRepository.SaveTakeProfitOrdersAsync"/>).
        /// </summary>
        /// <remarks>
        /// Despite its singular name, this method always saves the entire
        /// <see cref="Trade.TakeProfitOrders"/> collection — not just the supplied order.
        /// If <paramref name="tpOrder"/> is new (<c>Id == 0</c>) it is appended to the collection
        /// before saving; if it already exists it is updated in place.
        /// </remarks>
        /// <param name="tpOrder">The take-profit order to add or update.</param>
        public async Task SaveTakeProfitOrderAsync(TakeProfitOrder tpOrder)
        {
            if (SelectedTrade == null)
                return;
            if (tpOrder.Id == 0)
            {
                SelectedTrade.TakeProfitOrders.Add(tpOrder);
                tpOrder.Trade = SelectedTrade;
            }
            await TradeRepository.SaveTakeProfitOrdersAsync(SelectedTrade);
            RefreshTrades();
        }
    }
}
