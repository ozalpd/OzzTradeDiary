using TD.Models;

namespace TD.SQLite
{
    public partial class TradeRepository
    {
        partial void OnLoaded(Trade trade)
        {
            _ = LoadNavigationCollections(trade);
        }

        public async Task LoadNavigationCollections(Trade trade)
        {
            try
            {
                var images = await _tradeImageRepository.GetByTradeIdAsync(trade.Id);
                trade.TradeImages = images.ToList();
            }
            catch (Exception)
            {
                // handle/log as needed
            }

            try
            {
                var entryOrders = await _entryOrderRepository.GetByTradeIdAsync(trade.Id);
                trade.EntryOrders = entryOrders.ToList();
            }
            catch (Exception)
            {
                // handle/log as needed
            }

            try
            {
                var tpOrders = await _takeProfitOrderRepository.GetByTradeIdAsync(trade.Id);
                trade.TakeProfitOrders = tpOrders.ToList();
            }
            catch (Exception)
            {
                // handle/log as needed
            }

            try
            {
                var slOrders = await _stopLossOrderRepository.GetByTradeIdAsync(trade.Id);
                trade.StopLossOrders = slOrders.ToList();
            }
            catch (Exception)
            {
                // handle/log as needed
            }
        }
    }

    public partial interface ITradeRepository
    {
        Task LoadNavigationCollections(Trade trade);
    }
}