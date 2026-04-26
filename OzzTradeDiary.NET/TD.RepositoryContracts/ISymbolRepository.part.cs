namespace TD.RepositoryContracts
{
    public partial interface ISymbolRepository
    {
        Task UpdateExchangeHasAnySymbolAsync(int exchangeId);
    }
}
