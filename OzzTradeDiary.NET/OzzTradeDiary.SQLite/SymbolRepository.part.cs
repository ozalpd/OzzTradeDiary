using TD.Models;

namespace TD.SQLite
{
    public partial class SymbolRepository
    {
        partial void OnCreated(Symbol symbol)
        {
            _ = UpdateExchangeHasAnySymbolAsync(symbol.ExchangeId);
        }

        private async Task UpdateExchangeHasAnySymbolAsync(int exchangeId)
        {
            try
            {
                await _exchangeRepository.UpdateHasAnySymbolAsync(exchangeId, true);
            }
            catch (Exception)
            {
                // handle/log as needed
            }
        }
    }
}
