using TD.AppInfra.Models;

namespace TD.AppInfra.DesignTime
{
    public class MockDataSources : AppDataSources
    {
        public MockDataSources() : base(new CurrencyMockRepository(), new ExchangeMockRepository(),
                new SymbolMockRepository(), new TradingAccountMockRepository(), new TradeMockRepository())
        {

        }
    }
}
