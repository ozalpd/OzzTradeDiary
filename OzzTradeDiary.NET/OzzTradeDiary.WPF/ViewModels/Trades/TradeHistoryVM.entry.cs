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
                EntryOrderEditCommand.RaiseCanExecuteChanged();
                EntryOrderDeleteCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(nameof(SelectedEntryOrder));
            }
        }
        EntryOrder? _selectedEntryOrder;

        /// <summary>
        /// Gets the collection of available EntryOrderType enum members for selection or display.
        /// </summary>
        public IEnumerable<EnumValueItem<EntryOrderType>> EntryOrderTypeValues { get; }

        public async Task LoadEntryOrdersAsync()
        {
            if (SelectedTrade != null)
                await LoadNavigationCollectionsAsync(SelectedTrade);
            ReplaceEntryOrders();
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
            if (entryOrder.Id <= 0)
                entryOrder.Id = await EntryOrderRepository.CreateAsync(entryOrder);
            else
                await EntryOrderRepository.UpdateAsync(entryOrder);
        }
    }
}
