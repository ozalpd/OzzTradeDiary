using TD.Models;

namespace TD.RepositoryContracts
{
    public partial interface ITradeRepository
    {
        Task LoadNavigationCollectionsAsync(Trade trade);
        Task SaveNavigationCollectionsAsync(Trade trade);
        Task SaveEntryOrdersAsync(Trade trade, bool updateTrade = true);
        Task SaveStopLossOrdersAsync(Trade trade, bool updateTrade = true);
        Task SaveTakeProfitOrdersAsync(Trade trade, bool updateTrade = true);
        Task SaveTradeImagesAsync(Trade trade, bool updateTrade = true);
    }
}
