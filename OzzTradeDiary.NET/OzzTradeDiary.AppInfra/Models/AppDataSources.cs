using TD.RepositoryContracts;

namespace TD.AppInfra.Models;

/// <summary>
/// Parameter object that groups all data-source dependencies for injection
/// into the application's composition root and ViewModels.
/// </summary>
public class AppDataSources
{
    public AppDataSources(ICurrencyRepository currencyRepository,
                          IExchangeRepository exchangeRepository,
                          ISymbolRepository symbolRepository,
                          ITradingAccountRepository tradingAccountRepository,
                          ITradeRepository tradeRepository)
    {
        CurrencyRepository = currencyRepository ?? throw new ArgumentNullException(nameof(currencyRepository));
        ExchangeRepository = exchangeRepository ?? throw new ArgumentNullException(nameof(exchangeRepository));
        SymbolRepository = symbolRepository ?? throw new ArgumentNullException(nameof(symbolRepository));
        TradingAccountRepository = tradingAccountRepository ?? throw new ArgumentNullException(nameof(tradingAccountRepository));
        TradeRepository = tradeRepository ?? throw new ArgumentNullException(nameof(tradeRepository));
    }

    public ICurrencyRepository CurrencyRepository { get; }
    public IExchangeRepository ExchangeRepository { get; }
    public ISymbolRepository SymbolRepository { get; }
    public ITradingAccountRepository TradingAccountRepository { get; }
    public ITradeRepository TradeRepository { get; }
}
