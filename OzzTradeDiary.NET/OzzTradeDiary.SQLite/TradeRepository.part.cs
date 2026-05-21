using TD.Models;

namespace TD.SQLite
{
    public partial class TradeRepository
    {
        public async Task LoadNavigationCollectionsAsync(Trade trade)
        {
            try
            {
                var images = await TradeImageRepository.GetByTradeIdAsync(trade.Id);
                trade.TradeImages = images.ToList();
            }
            catch (Exception)
            {
                // handle/log as needed
            }

            try
            {
                var entryOrders = await EntryOrderRepository.GetByTradeIdAsync(trade.Id);
                trade.EntryOrders = entryOrders.ToList();
            }
            catch (Exception)
            {
                // handle/log as needed
            }

            try
            {
                var tpOrders = await TakeProfitOrderRepository.GetByTradeIdAsync(trade.Id);
                trade.TakeProfitOrders = tpOrders.ToList();
            }
            catch (Exception)
            {
                // handle/log as needed
            }

            try
            {
                var slOrders = await StopLossOrderRepository.GetByTradeIdAsync(trade.Id);
                trade.StopLossOrders = slOrders.ToList();
            }
            catch (Exception)
            {
                // handle/log as needed
            }
        }

        partial void OnLoaded(Trade trade)
        {
            _ = LoadNavigationCollectionsAsync(trade);
        }
    }
}