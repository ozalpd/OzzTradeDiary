using TD.AppInfra.Models;

namespace TD.AppInfra.DesignTime
{
    public class MockDataSources : AppDataSources
    {
        public MockDataSources() : base(new CurrencyMockRepository(), new EntryOrderMockRepository(), new ExchangeMockRepository(),
                                        new StopLossOrderMockRepository(), new SymbolMockRepository(), new TakeProfitOrderMockRepository(),
                                        new TradingAccountMockRepository(), new TradeMockRepository())
        {

        }
    }
}
