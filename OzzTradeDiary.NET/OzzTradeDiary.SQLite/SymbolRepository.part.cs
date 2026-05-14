using TD.Models;

namespace TD.SQLite
{
    public partial class SymbolRepository
    {
        partial void OnCreated(Symbol symbol)
        {
            _ = UpdateExchangeHasAnySymbolAsync(symbol.ExchangeId);
        }

        public async Task UpdateExchangeHasAnySymbolAsync(int exchangeId)
        {
            try
            {
                await ExchangeRepository.UpdateHasAnySymbolAsync(exchangeId, true);
            }
            catch (Exception)
            {
                // handle/log as needed
            }
        }
    }
}