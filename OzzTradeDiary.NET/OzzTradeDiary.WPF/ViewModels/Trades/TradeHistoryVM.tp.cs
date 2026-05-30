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

        public async Task SaveTakeProfitOrderAsync(TakeProfitOrder takeProfitOrder)
        {
            if (takeProfitOrder.Id <= 0)
                takeProfitOrder.Id = await TakeProfitOrderRepository.CreateAsync(takeProfitOrder);
            else
                await TakeProfitOrderRepository.UpdateAsync(takeProfitOrder);
        }
    }
}
