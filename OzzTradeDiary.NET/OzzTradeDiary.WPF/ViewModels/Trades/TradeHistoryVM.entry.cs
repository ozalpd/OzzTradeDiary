using System.Collections.ObjectModel;
using TD.Extensions;
using TD.Models;
using TD.RepositoryContracts;
using TD.WPF.Commands.Trades;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeHistoryVM
    {
        public IEntryOrderRepository EntryOrderRepository { get; }
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
        /// Gets the collection of available EntryOrderType enum members for selection or display.
        /// </summary>
        public IEnumerable<EnumValueItem<EntryOrderType>> EntryOrderTypeValues { get; }

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
                ReplaceCollection(EntryOrders, SelectedTrade.EntryOrders);

            RaiseEntryOrderCmdCanExecute();
        }

        private void RaiseEntryOrderCmdCanExecute()
        {
            EntryOrderCreateCommand.RaiseCanExecuteChanged();
            EntryOrderDeleteCommand.RaiseCanExecuteChanged();
            EntryOrderEditCommand.RaiseCanExecuteChanged();
        }

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
