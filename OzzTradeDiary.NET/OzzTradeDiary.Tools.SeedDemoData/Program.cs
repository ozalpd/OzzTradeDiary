using TD.Models;
using TD.SQLite;
using TD.WPF.Models;

var options = SeedOptions.Parse(args);
if (options.ShowHelp)
{
    SeedOptions.PrintUsage();
    return 0;
}

var settings = AppSettings.GetAppSettings();
var databasePath = settings?.DatabasePath ?? GetDefaultDebugDatabasePath();
Console.WriteLine($"Using database: {databasePath}");

if (options.Reset && File.Exists(databasePath))
{
    File.Delete(databasePath);
    Console.WriteLine("Existing debug database deleted.");
}

await SeedDemoDataAsync(databasePath);
Console.WriteLine("Demo data seeding completed.");
return 0;

static async Task SeedDemoDataAsync(string databasePath)
{
    Directory.CreateDirectory(Path.GetDirectoryName(databasePath)!);

    var exchangeRepository = new ExchangeRepository(databasePath);
    var symbolRepository = new SymbolRepository(databasePath, exchangeRepository);
    var tradingAccountRepository = new TradingAccountRepository(databasePath, exchangeRepository);
    var tradeImageRepository = new TradeImageRepository(databasePath);
    var tradeRepository = new TradeRepository(databasePath, tradingAccountRepository, symbolRepository, tradeImageRepository: tradeImageRepository);

    var exchange = await EnsureDemoExchangeAsync(exchangeRepository);
    var tradingAccount = await EnsureDemoTradingAccountAsync(tradingAccountRepository, exchange.Id);
    var tickers = new[] { "BTCUSD", "ETHUSD", "SOLUSD", "AVAXUSD", "XRPUSD", "ETCUSD", "DOGEUSD", "BNBUSD", "SUIUSD", "ZROUSD", //← Top 10 popular cryptos
                          "APTUSD", "ENAUSD", "ONDOUSD", "EIGENUSD", "SWELLUSD", "PENGUUSD", "POPCATUSD", "LUNAUSD" };
    for (int i = 0; i < tickers.Length; i++)
    {
        var ticker = tickers[i];
        var symbol = await EnsureDemoSymbolAsync(symbolRepository, exchange, ticker);
        int tradesCount = 88 - i * 5; // Deccreasing number of trades for each symbol
        tradesCount = tradesCount < 3 ? 3 : tradesCount; // Minimum 3 trades per symbol
        for (int j = 0; j < tradesCount; j++)
        {
            var daysAgo = (3 + tradesCount) - j;
            var trade = await EnsureDemoTradeAsync(tradeRepository, tradeImageRepository, tradingAccount.Id, symbol.Id, daysAgo);
        }
    }
}

static async Task<Exchange> EnsureDemoExchangeAsync(IExchangeRepository exchangeRepository)
{
    const string exchangeCode = "DEMO";
    var existing = await exchangeRepository.GetByExchangeCodeAsync(exchangeCode);
    if (existing is not null)
    {
        Console.WriteLine($"Exchange already exists: {existing.ExchangeCode}");
        return existing;
    }

    var exchange = new Exchange
    {
        ExchangeName = "Demo Exchange",
        ExchangeCode = exchangeCode,
        DefaultCurrency = "USD",
        HasAnySymbol = false,
        DisplayOrder = 9990,
        IsActive = true
    };

    exchange.Id = await exchangeRepository.CreateAsync(exchange);
    Console.WriteLine($"Created exchange: {exchange.ExchangeCode}");
    return exchange;
}

static async Task<Symbol> EnsureDemoSymbolAsync(ISymbolRepository symbolRepository, Exchange exchange, string ticker)
{
    string tickerFull = $"{exchange.ExchangeCode}:{ticker}";
    var existing = await symbolRepository.GetByTickerFullAsync(tickerFull);
    if (existing is not null)
    {
        Console.WriteLine($"Symbol already exists: {existing.TickerFull}");
        return existing;
    }
    int tickerLength = ticker.Length;
    var symbol = new Symbol
    {
        Ticker = ticker,
        TickerFull = tickerFull,
        BaseCurrency = ticker.Substring(0, ticker.Length - 3),
        PriceCurrency = ticker.Substring(ticker.Length - 3, 3),
        Description = $"Demo {ticker} symbol for local debugging",
        ExchangeId = exchange.Id,
        MarketType = MarketType.Crypto,
        DisplayOrder = 9990,
        IsActive = true
    };

    symbol.Id = await symbolRepository.CreateAsync(symbol);
    Console.WriteLine($"Created symbol: {symbol.TickerFull}");
    return symbol;
}

static async Task<TradingAccount> EnsureDemoTradingAccountAsync(ITradingAccountRepository tradingAccountRepository, int exchangeId)
{
    const string title = "Demo Trading Account";
    var existing = await tradingAccountRepository.GetByTitleAsync(title);
    if (existing is not null)
    {
        Console.WriteLine($"Trading account already exists: {existing.Title}");
        return existing;
    }

    var tradingAccount = new TradingAccount
    {
        Title = title,
        ExchangeId = exchangeId,
        Notes = "Local debug/demo account",
        DisplayOrder = 9990,
        IsActive = true
    };

    tradingAccount.Id = await tradingAccountRepository.CreateAsync(tradingAccount);
    Console.WriteLine($"Created trading account: {tradingAccount.Title}");
    return tradingAccount;
}

static async Task<Trade> EnsureDemoTradeAsync(ITradeRepository tradeRepository, TradeImageRepository tradeImageRepository, int tradingAccountId, int symbolId, int daysAgo)
{
    var random = new Random();
    decimal entryPrice = 60000m + random.Next(-5000, 5000);
    var trade = new Trade
    {
        TradingAccountId = tradingAccountId,
        SymbolId = symbolId,
        EntryTime = DateTime.UtcNow.AddDays(-daysAgo).AddHours(random.Next(0, 12)).AddMinutes(random.Next(0, 60)),
        EntryMethod = EntryMethod.Market,
        TradeDirection = TradeDirection.Long,
        PlannedEntry = entryPrice,
        ExecutedEntry = entryPrice,
        PlannedTP = entryPrice * 1.04m,
        ExecutedTP = 0m,
        PlannedSL = entryPrice * 0.98m,
        ExecutedSL = 0m,
        UpdatedAt = DateTime.UtcNow
    };

    trade.Id = await tradeRepository.CreateAsync(trade);
    Console.WriteLine($"Created trade: {trade.Id}");

    var randomInt = random.Next(1, 5);
    for (int i = 0; i < randomInt; i++)
    {
        var image = new TradeImage
        {
            TradeId = trade.Id,
            ImageURL = $"https://example.com/demo-trade-{i + 1}.png",
            Notes = $"Demo trade screenshot placeholder {i + 1}",
            UpdatedAt = DateTime.UtcNow
        };

        image.Id = await tradeImageRepository.CreateAsync(image);
        Console.WriteLine($"Created trade image: {image.Id}");
    }

    //await EnsureDemoTradeImageAsync(tradeImageRepository, trade.Id);

    return trade;
}

static string GetDefaultDebugDatabasePath()
{
    var current = new DirectoryInfo(AppContext.BaseDirectory);
    while (current is not null)
    {
        var hasGitFolder = Directory.Exists(Path.Combine(current.FullName, ".git"));
        var hasSolution = File.Exists(Path.Combine(current.FullName, "OzzTradeDiary.slnx"));

        if (hasGitFolder || hasSolution)
        {
            return Path.Combine(current.FullName, "SampleData", "trades.db");
        }

        current = current.Parent;
    }

    return Path.Combine(AppContext.BaseDirectory, "SampleData", "trades.db");
}

sealed class SeedOptions
{
    public string? DatabasePath { get; private set; }
    public bool Reset { get; private set; }
    public bool ShowHelp { get; private set; }

    public static SeedOptions Parse(string[] args)
    {
        var options = new SeedOptions();

        for (var i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--db":
                    if (i + 1 < args.Length)
                    {
                        options.DatabasePath = args[++i];
                    }
                    break;
                case "--reset":
                    options.Reset = true;
                    break;
                case "--help":
                case "-h":
                case "/?":
                    options.ShowHelp = true;
                    break;
            }
        }

        return options;
    }

    public static void PrintUsage()
    {
        Console.WriteLine("Seeds demo data into the debug SQLite database.");
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("  --db <path>   Use a specific SQLite database path.");
        Console.WriteLine("  --reset       Delete the existing database file before seeding.");
        Console.WriteLine("  --help        Show this help.");
    }
}
