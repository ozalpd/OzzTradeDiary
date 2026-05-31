using TD.Models;

namespace TD.SQLite
{
    public partial class TradeRepository
    {
        partial void OnCreated(Trade trade)
        {
            _ = SaveNavigationCollectionsAsync(trade);
        }

        partial void OnLoaded(Trade trade)
        {
            _ = LoadNavigationCollectionsAsync(trade);
        }

        partial void OnUpdated(Trade trade)
        {
            _ = SaveNavigationCollectionsAsync(trade);
        }

        public async Task LoadNavigationCollectionsAsync(Trade trade)
        {
            try
            {
                var images = await TradeImageRepository.GetByTradeIdAsync(trade.Id);
                trade.TradeImages = images.ToList();
                foreach (var image in trade.TradeImages)
                {
                    image.Trade = trade;
                }
            }
            catch (Exception)
            {
                // handle/log as needed
            }

            try
            {
                var entryOrders = await EntryOrderRepository.GetByTradeIdAsync(trade.Id);
                trade.EntryOrders = entryOrders.ToList();
                foreach (var order in trade.EntryOrders)
                {
                    order.Trade = trade;
                }
            }
            catch (Exception)
            {
                // handle/log as needed
            }

            try
            {
                var tpOrders = await TakeProfitOrderRepository.GetByTradeIdAsync(trade.Id);
                trade.TakeProfitOrders = tpOrders.ToList();
                foreach (var item in trade.TakeProfitOrders)
                {
                    item.Trade = trade;
                }
            }
            catch (Exception)
            {
                // handle/log as needed
            }

            try
            {
                var slOrders = await StopLossOrderRepository.GetByTradeIdAsync(trade.Id);
                trade.StopLossOrders = slOrders.ToList();
                foreach (var item in trade.StopLossOrders)
                {
                    item.Trade = trade;
                }
            }
            catch (Exception)
            {
                // handle/log as needed
            }
        }

        public async Task SaveNavigationCollectionsAsync(Trade trade)
        {
            if (trade == null || updatingFromSaveNavigationCollectionsAsync)
                return;

            updatingFromSaveNavigationCollectionsAsync = true;
            try
            {
                await SaveTradeImagesAsync(trade);
                await SaveEntryOrdersAsync(trade, updateTrade: false);
                await SaveTakeProfitOrdersAsync(trade, updateTrade: false);
                await SaveStopLossOrdersAsync(trade, updateTrade: false);

                trade.CalculateFromOrders();
                await UpdateAsync(trade);
            }
            finally
            {
                updatingFromSaveNavigationCollectionsAsync = false;
            }
        }
        bool updatingFromSaveNavigationCollectionsAsync = false;

        public async Task SaveEntryOrdersAsync(Trade trade, bool updateTrade = true)
        {
            bool anyChanges = false;
            var existingOrders = await EntryOrderRepository.GetByTradeIdAsync(trade.Id);
            var existingOrderIds = existingOrders.Select(o => o.Id).ToHashSet();
            foreach (var order in trade.EntryOrders)
            {
                if (order.Id == 0)
                {
                    order.TradeId = trade.Id;
                    await EntryOrderRepository.CreateAsync(order);
                    anyChanges = true;
                }
                else if (existingOrderIds.Contains(order.Id))
                {
                    anyChanges = await EntryOrderRepository.UpdateAsync(order) || anyChanges;
                    existingOrderIds.Remove(order.Id);
                }
            }
            foreach (var id in existingOrderIds)
            {
                await EntryOrderRepository.DeleteAsync(id);
                anyChanges = true;
            }

            if (anyChanges && updateTrade)
            {
                trade.CalculateFromOrders();
                await UpdateAsync(trade);
            }
        }

        public async Task SaveStopLossOrdersAsync(Trade trade, bool updateTrade = true)
        {
            bool anyChanges = false;
            var existingOrders = await StopLossOrderRepository.GetByTradeIdAsync(trade.Id);
            var existingOrderIds = existingOrders.Select(o => o.Id).ToHashSet();
            foreach (var order in trade.StopLossOrders)
            {
                if (order.Id == 0)
                {
                    order.TradeId = trade.Id;
                    await StopLossOrderRepository.CreateAsync(order);
                    anyChanges = true;
                }
                else if (existingOrderIds.Contains(order.Id))
                {
                    anyChanges = await StopLossOrderRepository.UpdateAsync(order) || anyChanges;
                    existingOrderIds.Remove(order.Id);
                }
            }
            foreach (var id in existingOrderIds)
            {
                await StopLossOrderRepository.DeleteAsync(id);
                anyChanges = true;
            }

            if (anyChanges && updateTrade)
            {
                trade.CalculateFromOrders();
                await UpdateAsync(trade);
            }
        }

        public async Task SaveTakeProfitOrdersAsync(Trade trade, bool updateTrade = true)
        {
            bool anyChanges = false;
            var existingOrders = await TakeProfitOrderRepository.GetByTradeIdAsync(trade.Id);
            var existingOrderIds = existingOrders.Select(o => o.Id).ToHashSet();
            foreach (var order in trade.TakeProfitOrders)
            {
                if (order.Id == 0)
                {
                    order.TradeId = trade.Id;
                    await TakeProfitOrderRepository.CreateAsync(order);
                    anyChanges = true;
                }
                else if (existingOrderIds.Contains(order.Id))
                {
                    anyChanges = await TakeProfitOrderRepository.UpdateAsync(order) || anyChanges;
                    existingOrderIds.Remove(order.Id);
                }
            }
            foreach (var id in existingOrderIds)
            {
                await TakeProfitOrderRepository.DeleteAsync(id);
                anyChanges = true;
            }

            if (anyChanges && updateTrade)
            {
                trade.CalculateFromOrders();
                await UpdateAsync(trade);
            }
        }

        private async Task SaveTradeImagesAsync(Trade trade)
        {
            var existingImages = await TradeImageRepository.GetByTradeIdAsync(trade.Id);
            var existingImageIds = existingImages.Select(i => i.Id).ToHashSet();
            foreach (var image in trade.TradeImages)
            {
                if (image.Id == 0)
                {
                    image.TradeId = trade.Id;
                    await TradeImageRepository.CreateAsync(image);
                }
                else if (existingImageIds.Contains(image.Id))
                {
                    await TradeImageRepository.UpdateAsync(image);
                    existingImageIds.Remove(image.Id);
                }
            }
            foreach (var id in existingImageIds)
            {
                await TradeImageRepository.DeleteAsync(id);
            }
        }
    }
}