using Microsoft.Data.Sqlite;
using TD.Helpers;
using TD.Models;
using TD.SQLite.Extensions;

namespace TD.SQLite
{
    public partial class TradeRepository
    {
        partial void OnAppendingWhere(TradeQueryParameters queryParameters, List<string> whereClauses, SqliteCommand command)
        {
            AppendTradeDatesToWhereClause(queryParameters, whereClauses, command);

            if (queryParameters.TradeStatus.HasValue && queryParameters.TradeStatus.Value is TradeStatusQuery stat && (int)stat != 0)
            {
                if ((int)stat < 1000)
                {
                    whereClauses.Add("TradeStatus = @tradeStatus");
                    command.AddParameter("@tradeStatus", (int)stat);
                }
                else if (stat == TradeStatusQuery.Waiting)
                {
                    whereClauses.Add("TradeStatus < @tradeStatusOpen");
                    command.AddParameter("@tradeStatusOpen", (int)TradeStatusQuery.Active);
                }
                else if (stat == TradeStatusQuery.ActiveOrWaiting)
                {
                    whereClauses.Add("TradeStatus < @tradeStatusClosed");
                    command.AddParameter("@tradeStatusClosed", (int)TradeStatusQuery.Closed);
                }
                else if (stat == TradeStatusQuery.MissedOrCancelled)
                {
                    whereClauses.Add("TradeStatus < @notMissedOrCancelled");
                    command.AddParameter("@notMissedOrCancelled", 0);
                }

                if (stat == TradeStatusQuery.Waiting || stat == TradeStatusQuery.ActiveOrWaiting)
                {
                    whereClauses.Add("TradeStatus > @notMissedOrCancelled");
                    command.AddParameter("@notMissedOrCancelled", 0);
                }
            }
        }

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

        private void AppendTradeDatesToWhereClause(TradeQueryParameters queryParameters, List<string> whereClauses, SqliteCommand command)
        {
            if (queryParameters?.TradeDateType == null)
                return;

            //queryParameters.EntryTimeMin = null; queryParameters.EntryTimeMax = null;
            //queryParameters.ExitTimeMin = null; queryParameters.ExitTimeMax = null;
            //queryParameters.UpdatedAtMin = null; queryParameters.UpdatedAtMax = null;

            switch (queryParameters.TradeDateType.Value)
            {
                case TradeDateType.EntryTime:
                    queryParameters.EntryTimeMin = queryParameters.StartDate;
                    queryParameters.EntryTimeMax = queryParameters.EndDate;
                    break;

                case TradeDateType.ExitTime:
                    queryParameters.ExitTimeMin = queryParameters.StartDate;
                    queryParameters.ExitTimeMax = queryParameters.EndDate;
                    break;

                case TradeDateType.CancellationTime:
                    queryParameters.CancellationTimeMin = queryParameters.StartDate;
                    queryParameters.CancellationTimeMax = queryParameters.EndDate;
                    break;

                case TradeDateType.UpdateTime:
                    queryParameters.UpdatedAtMin = queryParameters.StartDate;
                    queryParameters.UpdatedAtMax = queryParameters.EndDate;
                    break;

                default:
                    break;
            }
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