using TD.Extensions;
using TD.Models;
using TD.RepositoryContracts;
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

await SeedDemoDataAsync(databasePath, options.DaysAgo ?? 60, options.NoImages);
Console.WriteLine("Demo data seeding completed.");
return 0;

static async Task SeedDemoDataAsync(string databasePath, int daysAgoStart, bool noImages)
{
    Directory.CreateDirectory(Path.GetDirectoryName(databasePath)!);

    var exchangeRepository = new ExchangeRepository(databasePath);
    if (exchangeRepository == null)
    {
        Console.WriteLine("Failed to create ExchangeRepository.");
        return;
    }

    var symbolRepository = new SymbolRepository(databasePath, exchangeRepository: exchangeRepository);
    var tradingAccountRepository = new TradingAccountRepository(databasePath, exchangeRepository: exchangeRepository);
    var tradeImageRepository = new TradeImageRepository(databasePath);
    var tradeRepository = new TradeRepository(databasePath, symbolRepository: symbolRepository, tradeImageRepository: tradeImageRepository, tradingAccountRepository: tradingAccountRepository);

    var exchange1 = await EnsureDemoExchangeAsync(exchangeRepository);
    var tickers = new[] { "BTCUSD", "ETHUSD", "SOLUSD", "AVAXUSD", "XRPUSD", "ETCUSD", "DOGEUSD", "BNBUSD", "SUIUSD", "ZROUSD", //← Top 10 popular cryptos
                          "APTUSD", "ENAUSD", "ONDOUSD", "EIGENUSD", "SWELLUSD", "PENGUUSD", "ADAUSD", "POPCATUSD", "LUNAUSD" };

    var currencyRepository = new CurrencyRepository(databasePath);
    for (int i = 0; i < tickers.Length; i++)
    {
        await EnsureDemoSymbolAsync(symbolRepository, currencyRepository, exchange1, tickers[i], 100 * (i + 1));
    }

    var tradingAccount1 = await EnsureDemoAccountAsync(tradingAccountRepository, exchange1, MarketType.CryptoSpot);
    await exchangeRepository.LoadNavigationCollections(exchange1);

    var exchange2 = await exchangeRepository.GetByExchangeCodeAsync("BYBIT");
    TradingAccount? tradingAccount2 = null;
    if (exchange2 != null)
        tradingAccount2 = await EnsureDemoAccountAsync(tradingAccountRepository, exchange2, MarketType.CryptoPerpetual);

    for (int i = daysAgoStart / 2; i > 0; i--)
    {
        int daysAgo = (i * 2) - 1;
        await seedTrades(tradingAccount1, tradeRepository, tradeImageRepository, exchange1, daysAgo, noImages);
        if (tradingAccount2 != null)
            await seedTrades(tradingAccount2, tradeRepository, tradeImageRepository, exchange2, daysAgo, noImages, "USDT.P");
        Console.WriteLine($"{daysAgo} days ago trades seeded for both exchanges.");
    }

    static async Task<bool> seedTrades(TradingAccount tradingAccount, ITradeRepository tradeRepository, TradeImageRepository tradeImageRepository, Exchange? exchange, int daysAgo, bool noImages, string suffix = "")
    {
        if (exchange == null)
            return false;

        var symbolSet = string.IsNullOrEmpty(suffix)
                      ? exchange.Symbols
                      : exchange.Symbols.Where(s => s.TickerFull.EndsWith(suffix)).ToList();
        if (symbolSet == null || symbolSet.Count == 0)
            return false;

        var random = new Random();
        int tradesCount = 5; // max 5 trades per symbol
        foreach (var symbol in symbolSet)
        {
            for (int j = 0; j < tradesCount; j++)
            {
                var trade = await EnsureDemoTradeAsync(tradeRepository, tradeImageRepository, tradingAccount.Id, symbol, daysAgo + tradesCount - j - 1, noImages, random);
            }

            tradesCount = tradesCount - 1; // Decrease the number of trades for each subsequent symbol to create variety, starting from 15 for the first symbol.
            tradesCount = tradesCount < 1 ? 1 : tradesCount;
        }

        Console.WriteLine($"Seeded trades for exchange: {exchange.ExchangeCode}, trading account: {tradingAccount.Title}");
        return true;
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
        DefaultCurrencyId = 2, //USDT
        HasAnySymbol = false,
        DisplayOrder = 9990,
        IsActive = true,
        CountryCode = "us",
        Timezone = "UTC"
    };

    exchange.Id = await exchangeRepository.CreateAsync(exchange);
    Console.WriteLine($"Created exchange: {exchange.ExchangeCode}");
    return exchange;
}

static async Task<Symbol?> EnsureDemoSymbolAsync(ISymbolRepository symbolRepository, ICurrencyRepository currencyRepository, Exchange exchange, string ticker, int displayOrder)
{
    string tickerFull = $"{exchange.ExchangeCode}:{ticker}";
    var existing = await symbolRepository.GetByTickerFullAsync(tickerFull);
    if (existing is not null)
    {
        Console.WriteLine($"Symbol already exists: {existing.TickerFull}");
        return existing;
    }
    int tickerLength = ticker.Length;
    var priceCurrency = await currencyRepository.GetByCurrencyTickerAsync(ticker.Substring(tickerLength - 3, 3));
    if (priceCurrency == null)
        return null;

    var symbol = new Symbol
    {
        Ticker = ticker,
        TickerFull = tickerFull,
        BaseCurrency = ticker.Substring(0, ticker.Length - 3),
        PriceCurrencyId = priceCurrency.Id,
        Description = $"Demo {ticker} symbol for local debugging",
        ExchangeId = exchange.Id,
        MarketType = MarketType.CryptoSpot,
        DisplayOrder = displayOrder,
        IsActive = true
    };

    symbol.Id = await symbolRepository.CreateAsync(symbol);
    Console.WriteLine($"Created symbol: {symbol.TickerFull}");
    return symbol;
}

static async Task<TradingAccount> EnsureDemoAccountAsync(ITradingAccountRepository tradingAccountRepository, Exchange exchange, MarketType marketType)
{
    string title = $"Demo {exchange.ExchangeCode} Account";
    var existing = await tradingAccountRepository.GetByTitleAsync(title);
    if (existing is not null)
    {
        Console.WriteLine($"Trading account already exists: {existing.Title}");
        return existing;
    }

    var tradingAccount = new TradingAccount
    {
        Title = title,
        ExchangeId = exchange.Id,
        Notes = "Local debug/demo account",
        DisplayOrder = 9990,
        MarketType = marketType,
        MakerFeeRate = 0.0002m,
        TakerFeeRate = 0.0005m,
        IsActive = true
    };

    tradingAccount.Id = await tradingAccountRepository.CreateAsync(tradingAccount);
    Console.WriteLine($"Created trading account: {tradingAccount.Title}");
    return tradingAccount;
}

static async Task<Trade> EnsureDemoTradeAsync(ITradeRepository tradeRepository, TradeImageRepository tradeImageRepository, int tradingAccountId, Symbol symbol, int daysAgo, bool noImages, Random? rng = null)
{
    var random = rng ?? new Random();
    var priceDict = GetCryptoPriceDict();
    var direction = random.Next(0, 3) == 0 //In real world, long trades are more common than short,
                  ? TradeDirection.Short   //so we can weight it a bit. 1/3 chance for short, 2/3 for long.
                  : TradeDirection.Long;
    string priceKey = symbol.BaseCurrency ?? symbol.Ticker.Replace("USDT.P", "").Replace("USDT", "").Replace("USDC", "").Replace("USD", "");
    decimal entryPrice;
    if (priceDict.ContainsKey(priceKey))
    {
        entryPrice = priceDict[priceKey] * (decimal)(1 + (random.NextDouble() * 0.6 - 0.3)); // Random variation between -0.3 and 0.3
    }
    else
    {
        entryPrice = 100m * (decimal)(1 + (random.NextDouble() * 0.9 - 0.45)); // Default price with random variation if not found in the dictionary
    }

    entryPrice = entryPrice.RoundToQuantum();
    decimal tp1Multiplier = direction == TradeDirection.Long ? 1.02m : 0.98m; // Take profit multiplier based on trade direction
    decimal tp2Multiplier = direction == TradeDirection.Long ? 1.04m : 0.96m; // Take profit multiplier based on trade direction
    decimal tp3Multiplier = direction == TradeDirection.Long ? 1.06m : 0.94m; // Take profit multiplier based on trade direction
    decimal slMultiplier = direction == TradeDirection.Long ? 0.98m : 1.02m; // Stop loss multiplier based on trade direction
    // 20% chance 3 entry orders, 40% chance 2, 40% chance 1
    int entryOrderCount = random.Next(0, 10) switch { < 2 => 3, < 6 => 2, _ => 1 };

    // For multi-entry DCA, each subsequent order is placed slightly better (lower for Long, higher for Short)
    decimal dcaStep = direction == TradeDirection.Long ? -0.01m : 0.01m;
    decimal totalQuantity = (1000m / entryPrice).RoundToQuantum(); // Total $1000 position across all entry orders
    decimal perOrderQuantity = (totalQuantity / entryOrderCount).RoundToQuantum();

    var entryOrders = new List<EntryOrder>();
    for (int e = 0; e < entryOrderCount; e++)
    {
        decimal orderPrice = (entryPrice * (1m + dcaStep * e)).RoundToQuantum();
        entryOrders.Add(new EntryOrder
        {
            OrderType = EntryOrderType.Limit,
            OrderPrice = orderPrice,
            OrderQuantity = perOrderQuantity,
            UpdatedAt = DateTime.UtcNow
        });
    }
    decimal quantity = perOrderQuantity * entryOrderCount; // Total planned quantity across all entry orders

    var slOrder = new StopLossOrder
    {
        OrderType = ExitOrderType.Stop,
        OrderPrice = (entryPrice * slMultiplier).RoundToQuantum(),
        OrderQuantity = quantity,
        UpdatedAt = DateTime.UtcNow
    };
    var tp1 = new TakeProfitOrder
    {
        OrderType = ExitOrderType.Limit,
        OrderPrice = (entryPrice * tp1Multiplier).RoundToQuantum(),
        OrderQuantity = quantity * 0.4m, // 40% of the quantity for TP1
        UpdatedAt = DateTime.UtcNow,
    };
    var tp2 = new TakeProfitOrder
    {
        OrderType = ExitOrderType.Limit,
        OrderPrice = (entryPrice * tp2Multiplier).RoundToQuantum(),
        OrderQuantity = quantity * 0.3m, // 30% of the quantity for TP2
        UpdatedAt = DateTime.UtcNow
    };
    var tp3 = new TakeProfitOrder
    {
        OrderType = ExitOrderType.TrailingStop,
        OrderPrice = (entryPrice * tp3Multiplier).RoundToQuantum(),
        OrderQuantity = quantity * 0.3m, // 30% of the quantity for TP3
        UpdatedAt = DateTime.UtcNow
    };

    var trade = new Trade
    {
        TradingAccountId = tradingAccountId,
        SymbolId = symbol.Id,
        MarketType = symbol.MarketType,
        TradeDirection = direction,
        UpdatedAt = DateTime.UtcNow,
    };
    foreach (var eo in entryOrders)
        trade.EntryOrders.Add(eo);
    trade.StopLossOrders.Add(slOrder);
    trade.TakeProfitOrders.Add(tp1);
    trade.TakeProfitOrders.Add(tp2);
    trade.TakeProfitOrders.Add(tp3);

    bool isExecuted = random.Next(0, 4) != 0; // 75% chance the trade is executed
    DateTime? entryTime = isExecuted
                        ? DateTime.UtcNow.AddDays(-daysAgo).AddHours(random.Next(0, 12)).AddMinutes(random.Next(0, 60))
                        : null;
    if (isExecuted && entryTime.HasValue)
    {
        trade.EntryTime = entryTime;
        // Fill entry orders with a slight time spread for multi-order trades
        for (int e = 0; e < entryOrders.Count; e++)
        {
            entryOrders[e].FilledPrice = entryOrders[e].OrderPrice;
            entryOrders[e].FilledQuantity = entryOrders[e].OrderQuantity;
            entryOrders[e].FilledTime = entryTime.Value.AddMinutes(e * random.Next(1, 30));
        }
    }

    bool isClosed = isExecuted && (daysAgo > 7 || random.Next(0, 4) == 0); // 25% chance the if trade is not older than 7 days
    if (isClosed && entryTime.HasValue)
    {
        trade.ExitTime = entryTime.Value.AddHours(random.Next(1, 72)); // Random exit time between 1 hour and 3 days after entry
        trade.TradeStatus = TradeStatus.Closed;

        bool isWin = random.Next(0, 2) == 0; // 50% chance of winning trade if it's closed
        bool hitTp2 = random.Next(0, 2) == 0; // 50% chance to hit TP2
        bool hitTp3 = hitTp2 && random.Next(0, 2) == 0; // 50% chance to hit TP3 if TP2 is hit
        decimal remainingQuantity = quantity;
        if (isWin)
        {
            tp1.FilledPrice = tp1.OrderPrice;
            tp1.FilledQuantity = tp1.OrderQuantity;
            tp1.FilledTime = entryTime.Value.AddHours(random.Next(1, 24));
            remainingQuantity = quantity - tp1.FilledQuantity.Value;
            if (hitTp2)
            {
                tp2.FilledPrice = tp2.OrderPrice;
                tp2.FilledQuantity = tp2.OrderQuantity;
                tp2.FilledTime = tp1.FilledTime.Value.AddHours(random.Next(1, 24));
                remainingQuantity -= tp2.FilledQuantity.Value;
                if (hitTp3)
                {
                    tp3.FilledPrice = random.Next(0, 5) == 0
                                    ? tp3.OrderPrice * tp2Multiplier // 20% chance that TP3 is hit at a better price than the order price, by applying the same multiplier again
                                    : random.Next(0, 10) == 0
                                    ? tp3.OrderPrice * tp3Multiplier // 10% chance that TP3 is hit at a much better price than the order price, by applying the TP3 multiplier
                                    : tp3.OrderPrice;
                    tp3.FilledQuantity = tp3.OrderQuantity;
                    tp3.FilledTime = tp2.FilledTime.Value.AddHours(random.Next(1, 24));
                }
            }
        }

        if (!isWin)
        {
            slOrder.FilledPrice = slOrder.OrderPrice;
            slOrder.FilledQuantity = remainingQuantity;
            slOrder.FilledTime = trade.ExitTime;
        }
        else if (!hitTp2) //If tp1 is hit but not tp2, we can assume executed SL price is between entry price and tp1
        {
            slOrder.FilledPrice = (tp1.FilledPrice + slOrder.OrderPrice) / 2;
            slOrder.FilledQuantity = remainingQuantity;
            slOrder.FilledTime = trade.ExitTime;
        }
        else if (!hitTp3) //If tp2 is hit but not tp3, we can assume executed SL price is between tp2 and tp3
        {
            slOrder.FilledPrice = (tp2.FilledPrice + tp3.OrderPrice) / 2;
            slOrder.FilledQuantity = remainingQuantity;
            slOrder.FilledTime = trade.ExitTime;
        }
    }
    else if (isExecuted)
    {
        trade.TradeStatus = TradeStatus.Active;
    }
    else
    {
        trade.TradeStatus = daysAgo < 7
                          ? random.Next(0, 2) == 0 ? TradeStatus.Pending : TradeStatus.Planned
                          : random.Next(0, 3) == 0 ? TradeStatus.Missed : TradeStatus.Cancelled;
        if (trade.TradeStatus == TradeStatus.Cancelled)
            trade.CancellationTime = DateTime.UtcNow.AddDays(-daysAgo).AddHours(random.Next(1, 6));
    }

    AppendDemoNotes(trade, rng);
    // Assign sample tags for variety
    var tagSets = new[] { "breakout", "pullback", "reversal", "trend", "scalp", "swing", "news", "fomo", "missed-entry", "high-rr" };
    trade.Tags = tagSets[random.Next(tagSets.Length)];

    trade.CalculateFromOrders();

    trade.Id = await tradeRepository.CreateAsync(trade);
    Console.WriteLine($"Created trade: {trade.Id} for {symbol.TickerFull} at {trade.EntryTime}");
    if (noImages)
        return trade;

    var imgNrList = new List<int> { 1, 2, 3, 4, 5, 6 };
    //scramble imgNrList
    imgNrList = [.. imgNrList.OrderBy(x => random.Next())];

    var settings = AppSettings.GetAppSettings();
    var randomInt = random.Next(1, 5);
    for (int i = 0; i < randomInt; i++)
    {
        int imgNr = imgNrList[i];
        var image = new TradeImage
        {
            TradeId = trade.Id,
            Category = random.Next(0, 2) == 0 ? TradeImageCategory.Setup : TradeImageCategory.Review,
            ImageURL = imgNr == 0 ? $"https://s3.tradingview.com/snapshots/v/VxW2WUbH.png" : Path.Combine(settings.GetDatabaseFolderPath(), "Images", $"Demo-{imgNr:D2}.png"),
            Notes = TD.Tools.Text.CreateLipsumParagraphs(random.Next(1, 3)),
            UpdatedAt = DateTime.UtcNow
        };

        image.Id = await tradeImageRepository.CreateAsync(image);
    }

    return trade;
}

static void AppendDemoNotes(Trade trade, Random? rng = null)
{
    var demoNotes = new[]
    {
        "This is a demo trade for local testing.",
        "Seeded by TD.Tools.SeedDemoData.",
        "Ignore this trade, it's just for debugging purposes.",
        "Demo data - not a real trade.",
        "Used for testing and development only."
    };
    trade.SetupNotes = string.Join(" ", demoNotes);

    if (trade.TradeStatus != TradeStatus.Closed)
        return;

    var random = rng ?? new Random();
    trade.ReviewNotes = TD.Tools.Text.CreateLipsumParagraphs(random.Next(1, 3));
}

static Dictionary<string, decimal> GetCryptoPriceDict()
{
    return new Dictionary<string, decimal>
    {
        ["BTC"] = 75000m,
        ["ETH"] = 2500m,
        ["SOL"] = 100m,
        ["AVAX"] = 10m,
        ["XRP"] = 2,
        ["ADA"] = 1.44m,
        ["ETC"] = 8.54m,
        ["DOGE"] = 0.095m,
        ["BNB"] = 500m,
        ["SUI"] = 1m,
        ["ZRO"] = 2.10m,
        ["APT"] = 3m,
        ["ENA"] = 0.0907m,
        ["ONDO"] = 0.25449m,
        ["EIGEN"] = 0.174m,
        ["SWELL"] = 0.001256m,
        ["PENGU"] = 0.007557m,
        ["POPCAT"] = 0.058m,
        ["LUNA"] = 0.0663m
    };
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

    public int? DaysAgo { get; private set; }
    public bool NoImages { get; private set; }
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
                case "--daysago":
                    if (i + 1 < args.Length && int.TryParse(args[++i], out var daysAgo))
                    {
                        options.DaysAgo = daysAgo;
                    }
                    break;
                case "--reset":
                    options.Reset = true;
                    break;

                case "--noimg":
                    options.NoImages = true;
                    break;

                case "--noimage":
                    options.NoImages = true;
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