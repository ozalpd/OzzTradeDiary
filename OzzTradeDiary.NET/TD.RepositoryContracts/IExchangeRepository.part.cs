using TD.Models;

namespace TD.RepositoryContracts
{
    public partial interface IExchangeRepository
    {
        Task LoadNavigationCollections(Exchange exchange);
    }
}
