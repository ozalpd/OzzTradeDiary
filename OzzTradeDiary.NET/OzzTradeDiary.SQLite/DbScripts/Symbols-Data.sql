/* TickerFull Symbol name with exchange prefix, e.g. 'BYBIT:BTCUSD.P'
   Ticker Symbol name without exchange prefix, e.g. 'BTCUSD.P'
   BaseCurrency The base currency of the symbol, e.g. 'BTC' for 'BTCUSD.P' but empty string for stocks eg 'NASDAQ:AAPL'
   PriceCurrency The price currency of the symbol, e.g. 'USDT' for 'BTCUSDT.P', 'JPY' for 'GBPJPY', 'USD' for 'NASDAQ:AAPL' other currencies for different stock markets
   ExchangeId The ID of the exchange where the symbol is listed, e.g. 1 for 'NYSE', 2 for 'NASDAQ', 6 for BYBIT, 5 for BINGX, 21 for BINANCE, 4 for BIST etc
   MarketType The type of market, its an enum at C# side, 20 for 'Stock', 50 for 'Forex', 80 for 'Crypto', 90 for 'CryptoPerpetual'
   DisplayOrder The order in which the symbol is displayed
   IsActive Indicates if the symbol is active (1) or inactive (0)
*/
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (1, 'NASDAQ:AAPL', 'AAPL', 'USD', 'USD', 'Apple Inc.', 2, 20, 1000, 1);

Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (2, 'BYBIT:BTCUSDT', 'BTCUSDT', 'BTC', 'USDT', 'Bitcoin Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (3, 'BYBIT:BTCUSDC', 'BTCUSDC', 'BTC', 'USDC', 'Bitcoin Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (4, 'BYBIT:BTCUSDT.P', 'BTCUSDT.P', 'BTC', 'USDT', 'Bitcoin Perpetual Contract', 6, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (5, 'BYBIT:BTCUSDC.P', 'BTCUSDC.P', 'BTC', 'USDC', 'Bitcoin Perpetual Contract', 6, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (6, 'BYBIT:ETHUSDT', 'ETHUSDT', 'ETH', 'USDT', 'Ethereum Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (7, 'BYBIT:ETHUSDC', 'ETHUSDC', 'ETH', 'USDC', 'Ethereum Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (8, 'BYBIT:ETHUSDT.P', 'ETHUSDT.P', 'ETH', 'USDT', 'Ethereum Perpetual Contract', 6, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (9, 'BYBIT:ETHUSDC.P', 'ETHUSDC.P', 'ETH', 'USDC', 'Ethereum Perpetual Contract', 6, 90, 1000, 1);

Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (10, 'BINGX:BTCUSDT', 'BTCUSDT', 'BTC', 'USDT', 'Bitcoin Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (11, 'BINGX:BTCUSDC', 'BTCUSDC', 'BTC', 'USDC', 'Bitcoin Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (12, 'BINGX:BTCUSDT.P', 'BTCUSDT.P', 'BTC', 'USDT', 'Bitcoin Perpetual Contract', 5, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (13, 'BINGX:BTCUSDC.P', 'BTCUSDC.P', 'BTC', 'USDC', 'Bitcoin Perpetual Contract', 5, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (14, 'BINGX:ETHUSDT', 'ETHUSDT', 'ETH', 'USDT', 'Ethereum Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (15, 'BINGX:ETHUSDC', 'ETHUSDC', 'ETH', 'USDC', 'Ethereum Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (16, 'BINGX:ETHUSDT.P', 'ETHUSDT.P', 'ETH', 'USDT', 'Ethereum Perpetual Contract', 5, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (17, 'BINGX:ETHUSDC.P', 'ETHUSDC.P', 'ETH', 'USDC', 'Ethereum Perpetual Contract', 5, 90, 1000, 1);

Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (18, 'BYBIT:AVAXUSDT', 'AVAXUSDT', 'AVAX', 'USDT', 'Avalanche Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (19, 'BYBIT:AVAXUSDC', 'AVAXUSDC', 'AVAX', 'USDC', 'Avalanche Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (20, 'BYBIT:AVAXUSDT.P', 'AVAXUSDT.P', 'AVAX', 'USDT', 'Avalanche Perpetual Contract', 6, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (21, 'BYBIT:AVAXUSDC.P', 'AVAXUSDC.P', 'AVAX', 'USDC', 'Avalanche Perpetual Contract', 6, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (22, 'BYBIT:SOLUSDT', 'SOLUSDT', 'SOL', 'USDT', 'Solana Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (23, 'BYBIT:SOLUSDC', 'SOLUSDC', 'SOL', 'USDC', 'Solana Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (24, 'BYBIT:SOLUSDT.P', 'SOLUSDT.P', 'SOL', 'USDT', 'Solana Perpetual Contract', 6, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (25, 'BYBIT:SOLUSDC.P', 'SOLUSDC.P', 'SOL', 'USDC', 'Solana Perpetual Contract', 6, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (26, 'BYBIT:XRPUSDT', 'XRPUSDT', 'XRP', 'USDT', 'XRP Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (27, 'BYBIT:XRPUSDC', 'XRPUSDC', 'XRP', 'USDC', 'XRP Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (28, 'BYBIT:XRPUSDT.P', 'XRPUSDT.P', 'XRP', 'USDT', 'XRP Perpetual Contract', 6, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (29, 'BYBIT:XRPUSDC.P', 'XRPUSDC.P', 'XRP', 'USDC', 'XRP Perpetual Contract', 6, 90, 1000, 1);

Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (30, 'BINGX:AVAXUSDT', 'AVAXUSDT', 'AVAX', 'USDT', 'Avalanche Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (31, 'BINGX:AVAXUSDC', 'AVAXUSDC', 'AVAX', 'USDC', 'Avalanche Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (32, 'BINGX:AVAXUSDT.P', 'AVAXUSDT.P', 'AVAX', 'USDT', 'Avalanche Perpetual Contract', 5, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (33, 'BINGX:AVAXUSDC.P', 'AVAXUSDC.P', 'AVAX', 'USDC', 'Avalanche Perpetual Contract', 5, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (34, 'BINGX:SOLUSDT', 'SOLUSDT', 'SOL', 'USDT', 'Solana Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (35, 'BINGX:SOLUSDC', 'SOLUSDC', 'SOL', 'USDC', 'Solana Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (36, 'BINGX:SOLUSDT.P', 'SOLUSDT.P', 'SOL', 'USDT', 'Solana Perpetual Contract', 5, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (37, 'BINGX:SOLUSDC.P', 'SOLUSDC.P', 'SOL', 'USDC', 'Solana Perpetual Contract', 5, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (38, 'BINGX:XRPUSDT', 'XRPUSDT', 'XRP', 'USDT', 'XRP Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (39, 'BINGX:XRPUSDC', 'XRPUSDC', 'XRP', 'USDC', 'XRP Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (40, 'BINGX:XRPUSDT.P', 'XRPUSDT.P', 'XRP', 'USDT', 'XRP Perpetual Contract', 5, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (41, 'BINGX:XRPUSDC.P', 'XRPUSDC.P', 'XRP', 'USDC', 'XRP Perpetual Contract', 5, 90, 1000, 1);
			