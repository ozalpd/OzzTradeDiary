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
    var symbol = await EnsureDemoSymbolAsync(symbolRepository, exchange.Id);
    var tradingAccount = await EnsureDemoTradingAccountAsync(tradingAccountRepository, exchange.Id);
    var trade = await EnsureDemoTradeAsync(tradeRepository, tradingAccount.Id, symbol.Id);
    await EnsureDemoTradeImageAsync(tradeImageRepository, trade.Id);
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

static async Task<Symbol> EnsureDemoSymbolAsync(ISymbolRepository symbolRepository, int exchangeId)
{
    const string tickerFull = "DEMO:BTCUSD";
    var existing = await symbolRepository.GetByTickerFullAsync(tickerFull);
    if (existing is not null)
    {
        Console.WriteLine($"Symbol already exists: {existing.TickerFull}");
        return existing;
    }

    var symbol = new Symbol
    {
        Ticker = "BTCUSD",
        TickerFull = tickerFull,
        BaseCurrency = "BTC",
        PriceCurrency = "USD",
        Description = "Demo Bitcoin symbol for local debugging",
        ExchangeId = exchangeId,
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

static async Task<Trade> EnsureDemoTradeAsync(ITradeRepository tradeRepository, int tradingAccountId, int symbolId)
{
    var existing = (await tradeRepository.GetByTradingAccountIdAsync(tradingAccountId))
        .FirstOrDefault(x => x.SymbolId == symbolId);

    if (existing is not null)
    {
        Console.WriteLine($"Trade already exists: {existing.Id}");
        return existing;
    }

    var trade = new Trade
    {
        TradingAccountId = tradingAccountId,
        SymbolId = symbolId,
        EntryTime = DateTime.UtcNow.AddDays(-7),
        EntryMethod = EntryMethod.Market,
        TradeDirection = TradeDirection.Long,
        PlannedEntry = 62000m,
        ExecutedEntry = 61850m,
        PlannedTP = 66000m,
        ExecutedTP = 0m,
        PlannedSL = 60000m,
        ExecutedSL = 0m,
        UpdatedAt = DateTime.UtcNow
    };

    trade.Id = await tradeRepository.CreateAsync(trade);
    Console.WriteLine($"Created trade: {trade.Id}");
    return trade;
}

static async Task EnsureDemoTradeImageAsync(ITradeImageRepository tradeImageRepository, int tradeId)
{
    var existing = (await tradeImageRepository.GetByTradeIdAsync(tradeId))
        .FirstOrDefault(x => string.Equals(x.ImageURL, "https://example.com/demo-trade.png", StringComparison.OrdinalIgnoreCase));

    if (existing is not null)
    {
        Console.WriteLine($"Trade image already exists: {existing.Id}");
        return;
    }

    var tradeImage = new TradeImage
    {
        TradeId = tradeId,
        ImageURL = "https://example.com/demo-trade.png",
        Notes = "Demo trade screenshot placeholder",
        UpdatedAt = DateTime.UtcNow
    };

    tradeImage.Id = await tradeImageRepository.CreateAsync(tradeImage);
    Console.WriteLine($"Created trade image: {tradeImage.Id}");
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
