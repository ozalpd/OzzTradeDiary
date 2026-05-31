using System.Collections.ObjectModel;
using TD.Extensions;
using TD.Models;
using TD.RepositoryContracts;
using TD.WPF.Commands.Trades;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeHistoryVM
    {
        public ITakeProfitOrderRepository TakeProfitOrderRepository { get; }
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

        public async Task LoadTakeProfitOrdersAsync()
        {
            if (SelectedTrade != null)
                await LoadNavigationCollectionsAsync(SelectedTrade);
            ReplaceTakeProfitOrders();
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
