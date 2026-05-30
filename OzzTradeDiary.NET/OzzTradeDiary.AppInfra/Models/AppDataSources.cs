using TD.RepositoryContracts;

namespace TD.AppInfra.Models;

/// <summary>
/// Parameter object that groups all data-source dependencies for injection
/// into the application's composition root and ViewModels.
/// </summary>
public class AppDataSources
{
    public AppDataSources(ICurrencyRepository currencyRepository, IEntryOrderRepository entryOrderRepository,
                          IExchangeRepository exchangeRepository, IStopLossOrderRepository stopLossOrderRepository,
                          ISymbolRepository symbolRepository, ITakeProfitOrderRepository takeProfitOrderRepository,
                          ITradingAccountRepository tradingAccountRepository,
                          ITradeRepository tradeRepository)
    {
        CurrencyRepository = currencyRepository ?? throw new ArgumentNullException(nameof(currencyRepository));
        ExchangeRepository = exchangeRepository ?? throw new ArgumentNullException(nameof(exchangeRepository));
        SymbolRepository = symbolRepository ?? throw new ArgumentNullException(nameof(symbolRepository));
        TradingAccountRepository = tradingAccountRepository ?? throw new ArgumentNullException(nameof(tradingAccountRepository));

        EntryOrderRepository = entryOrderRepository ?? throw new ArgumentNullException(nameof(entryOrderRepository));
        TakeProfitOrderRepository = takeProfitOrderRepository ?? throw new ArgumentNullException(nameof(takeProfitOrderRepository));
        StopLossOrderRepository = stopLossOrderRepository ?? throw new ArgumentNullException(nameof(stopLossOrderRepository));
        TradeRepository = tradeRepository ?? throw new ArgumentNullException(nameof(tradeRepository));
    }

    public ICurrencyRepository CurrencyRepository { get; }
    public IExchangeRepository ExchangeRepository { get; }
    public ISymbolRepository SymbolRepository { get; }
    public ITradingAccountRepository TradingAccountRepository { get; }

    public IEntryOrderRepository EntryOrderRepository { get; }
    public ITakeProfitOrderRepository TakeProfitOrderRepository { get; }
    public IStopLossOrderRepository StopLossOrderRepository { get; }
    public ITradeRepository TradeRepository { get; }
}
