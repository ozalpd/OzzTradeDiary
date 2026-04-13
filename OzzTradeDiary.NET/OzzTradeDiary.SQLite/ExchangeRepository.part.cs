using TD.Models;

namespace TD.SQLite
{
    public partial class ExchangeRepository
    {
        partial void OnLoaded(Exchange exchange)
        {
            _ = LoadNavigationCollections(exchange);
        }

        public async Task LoadNavigationCollections(Exchange exchange)
        {
            try
            {
                var symbols = await _symbolRepository.GetByExchangeIdAsync(exchange.Id);
                exchange.Symbols = symbols.ToList();
            }
            catch (Exception)
            {
                // handle/log as needed
            }

            try
            {
                var tradingAcccounts = await _tradingAccountRepository.GetByExchangeIdAsync(exchange.Id);
                exchange.TradingAccounts = tradingAcccounts.ToList();
            }
            catch (Exception)
            {
                // handle/log as needed
            }
        }
    }
}
