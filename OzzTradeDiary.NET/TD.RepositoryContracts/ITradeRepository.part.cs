using TD.Models;

namespace TD.RepositoryContracts
{
    public partial interface ITradeRepository
    {
        Task LoadNavigationCollectionsAsync(Trade trade);
    }
}
