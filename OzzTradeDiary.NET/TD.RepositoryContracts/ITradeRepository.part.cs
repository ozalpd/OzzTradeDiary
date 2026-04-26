using TD.Models;

namespace TD.RepositoryContracts
{
    public partial interface ITradeRepository
    {
        Task LoadNavigationCollections(Trade trade);
    }
}
