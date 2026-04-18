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
			values (1, 'NASDAQ:AAPL', 'AAPL', null, 'USD', 'Apple Inc.', 2, 20, 1000, 1);

Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (2, 'BYBIT:BTCUSDT', 'BTCUSDT', 'BTC', 'USDT', 'Bitcoin Spot', 6, 80, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (3, 'BYBIT:BTCUSDC', 'BTCUSDC', 'BTC', 'USDC', 'Bitcoin Spot', 6, 80, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (4, 'BYBIT:BTCUSDT.P', 'BTCUSDT.P', 'BTC', 'USDT', 'Bitcoin Perpetual Contract', 6, 90, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (5, 'BYBIT:BTCUSDC.P', 'BTCUSDC.P', 'BTC', 'USDC', 'Bitcoin Perpetual Contract', 6, 90, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (6, 'BYBIT:ETHUSDT', 'ETHUSDT', 'ETH', 'USDT', 'Ethereum Spot', 6, 80, 200, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (7, 'BYBIT:ETHUSDC', 'ETHUSDC', 'ETH', 'USDC', 'Ethereum Spot', 6, 80, 200, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (8, 'BYBIT:ETHUSDT.P', 'ETHUSDT.P', 'ETH', 'USDT', 'Ethereum Perpetual Contract', 6, 90, 200, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (9, 'BYBIT:ETHUSDC.P', 'ETHUSDC.P', 'ETH', 'USDC', 'Ethereum Perpetual Contract', 6, 90, 200, 1);

Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (10, 'BINGX:BTCUSDT', 'BTCUSDT', 'BTC', 'USDT', 'Bitcoin Spot', 5, 80, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (11, 'BINGX:BTCUSDC', 'BTCUSDC', 'BTC', 'USDC', 'Bitcoin Spot', 5, 80, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (12, 'BINGX:BTCUSDT.P', 'BTCUSDT.P', 'BTC', 'USDT', 'Bitcoin Perpetual Contract', 5, 90, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (13, 'BINGX:BTCUSDC.P', 'BTCUSDC.P', 'BTC', 'USDC', 'Bitcoin Perpetual Contract', 5, 90, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (14, 'BINGX:ETHUSDT', 'ETHUSDT', 'ETH', 'USDT', 'Ethereum Spot', 5, 80, 200, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (15, 'BINGX:ETHUSDC', 'ETHUSDC', 'ETH', 'USDC', 'Ethereum Spot', 5, 80, 200, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (16, 'BINGX:ETHUSDT.P', 'ETHUSDT.P', 'ETH', 'USDT', 'Ethereum Perpetual Contract', 5, 90, 200, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (17, 'BINGX:ETHUSDC.P', 'ETHUSDC.P', 'ETH', 'USDC', 'Ethereum Perpetual Contract', 5, 90, 200, 1);

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

Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (42, 'BIST:AEFES', 'AEFES', null, 'TRY', 'Anadolu Efes Biracılık ve Malt Sanayii A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (43, 'BIST:AGHOL', 'AGHOL', null, 'TRY', 'Anadolu Grubu Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (44, 'BIST:AKBNK', 'AKBNK', null, 'TRY', 'Akbank T.A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (45, 'BIST:AKSA', 'AKSA', null, 'TRY', 'Aksa Akrilik Kimya Sanayii A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (46, 'BIST:AKSEN', 'AKSEN', null, 'TRY', 'Aksa Enerji Üretim A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (47, 'BIST:ALARK', 'ALARK', null, 'TRY', 'Alarko Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (48, 'BIST:ALTNY', 'ALTNY', null, 'TRY', 'Altınay Savunma Teknolojileri A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (49, 'BIST:ANSGR', 'ANSGR', null, 'TRY', 'Anadolu Sigorta A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (50, 'BIST:ARCLK', 'ARCLK', null, 'TRY', 'Arçelik A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (51, 'BIST:ASELS', 'ASELS', null, 'TRY', 'Aselsan Elektronik Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (52, 'BIST:ASTOR', 'ASTOR', null, 'TRY', 'Astor Enerji A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (53, 'BIST:BALSU', 'BALSU', null, 'TRY', 'Balsu Gıda Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (54, 'BIST:BIMAS', 'BIMAS', null, 'TRY', 'BİM Birleşik Mağazalar A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (55, 'BIST:BRSAN', 'BRSAN', null, 'TRY', 'Borusan Boru Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (56, 'BIST:BRYAT', 'BRYAT', null, 'TRY', 'Borusan Yatırım ve Pazarlama A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (57, 'BIST:BSOKE', 'BSOKE', null, 'TRY', 'Batısöke Söke Çimento Sanayii T.A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (58, 'BIST:BTCIM', 'BTCIM', null, 'TRY', 'Batıçim Batı Anadolu Çimento Sanayii A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (59, 'BIST:CANTE', 'CANTE', null, 'TRY', 'Çan2 Termik A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (60, 'BIST:CCOLA', 'CCOLA', null, 'TRY', 'Coca-Cola İçecek A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (61, 'BIST:CIMSA', 'CIMSA', null, 'TRY', 'Çimsa Çimento Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (62, 'BIST:CWENE', 'CWENE', null, 'TRY', 'CW Enerji Mühendislik Ticaret ve Sanayi A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (63, 'BIST:DAPGM', 'DAPGM', null, 'TRY', 'DAP Gayrimenkul Geliştirme A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (64, 'BIST:DOAS', 'DOAS', null, 'TRY', 'Doğuş Otomotiv Servis ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (65, 'BIST:DOHOL', 'DOHOL', null, 'TRY', 'Doğan Şirketler Grubu Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (66, 'BIST:DSTKF', 'DSTKF', null, 'TRY', 'Destek Finans Faktoring A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (67, 'BIST:ECILC', 'ECILC', null, 'TRY', 'EİS Eczacıbaşı İlaç, Sınai ve Finansal Yatırımlar Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (68, 'BIST:EFOR', 'EFOR', null, 'TRY', 'Efor Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (69, 'BIST:EGEEN', 'EGEEN', null, 'TRY', 'Ege Endüstri ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (70, 'BIST:EKGYO', 'EKGYO', null, 'TRY', 'Emlak Konut Gayrimenkul Yatırım Ortaklığı A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (71, 'BIST:ENERY', 'ENERY', null, 'TRY', 'Enerya Enerji A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (72, 'BIST:ENJSA', 'ENJSA', null, 'TRY', 'Enerjisa Enerji A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (73, 'BIST:ENKAI', 'ENKAI', null, 'TRY', 'Enka İnşaat ve Sanayi A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (74, 'BIST:EREGL', 'EREGL', null, 'TRY', 'Ereğli Demir ve Çelik Fabrikaları T.A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (75, 'BIST:EUPWR', 'EUPWR', null, 'TRY', 'Europower Enerji ve Otomasyon Teknolojileri Sanayi Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (76, 'BIST:FENER', 'FENER', null, 'TRY', 'Fenerbahçe Futbol A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (77, 'BIST:FROTO', 'FROTO', null, 'TRY', 'Ford Otomotiv Sanayi A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (78, 'BIST:GARAN', 'GARAN', null, 'TRY', 'Türkiye Garanti Bankası A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (79, 'BIST:GENIL', 'GENIL', null, 'TRY', 'Gen İlaç ve Sağlık Ürünleri Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (80, 'BIST:GESAN', 'GESAN', null, 'TRY', 'Girişim Elektrik Sanayi Taahhüt ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (81, 'BIST:GLRMK', 'GLRMK', null, 'TRY', 'Gülermak Ağır Sanayi İnşaat ve Taahhüt A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (82, 'BIST:GRSEL', 'GRSEL', null, 'TRY', 'Gür-Sel Turizm Taşımacılık ve Servis Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (83, 'BIST:GRTHO', 'GRTHO', null, 'TRY', 'Graintürk Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (84, 'BIST:GSRAY', 'GSRAY', null, 'TRY', 'Galatasaray Sportif Sınai ve Ticari Yatırımlar A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (85, 'BIST:GUBRF', 'GUBRF', null, 'TRY', 'Gübre Fabrikaları T.A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (86, 'BIST:HALKB', 'HALKB', null, 'TRY', 'Türkiye Halk Bankası A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (87, 'BIST:HEKTS', 'HEKTS', null, 'TRY', 'Hektaş Ticaret T.A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (88, 'BIST:ISCTR', 'ISCTR', null, 'TRY', 'Türkiye İş Bankası A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (89, 'BIST:ISMEN', 'ISMEN', null, 'TRY', 'İş Yatırım Menkul Değerler A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (90, 'BIST:IZENR', 'IZENR', null, 'TRY', 'İzdemir Enerji Elektrik Üretim A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (91, 'BIST:KCAER', 'KCAER', null, 'TRY', 'Kocaer Çelik Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (92, 'BIST:KCHOL', 'KCHOL', null, 'TRY', 'Koç Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (93, 'BIST:KLRHO', 'KLRHO', null, 'TRY', 'Kiler Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (94, 'BIST:KONTR', 'KONTR', null, 'TRY', 'Kontrolmatik Teknoloji Enerji ve Mühendislik A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (95, 'BIST:KRDMD', 'KRDMD', null, 'TRY', 'Kardemir Karabük Demir Çelik Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (96, 'BIST:KTLEV', 'KTLEV', null, 'TRY', 'Katılımevim Tasarruf Finansman A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (97, 'BIST:KUYAS', 'KUYAS', null, 'TRY', 'Kuyas Yatırım A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (98, 'BIST:MAGEN', 'MAGEN', null, 'TRY', 'Margün Enerji Üretim Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (99, 'BIST:MAVI', 'MAVI', null, 'TRY', 'Mavi Giyim Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (100, 'BIST:MGROS', 'MGROS', null, 'TRY', 'Migros Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (101, 'BIST:MIATK', 'MIATK', null, 'TRY', 'Mia Teknoloji A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (102, 'BIST:MPARK', 'MPARK', null, 'TRY', 'MLP Sağlık Hizmetleri A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (103, 'BIST:OBAMS', 'OBAMS', null, 'TRY', 'Oba Makarnacılık Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (104, 'BIST:ODAS', 'ODAS', null, 'TRY', 'Odaş Elektrik Üretim Sanayi Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (105, 'BIST:OTKAR', 'OTKAR', null, 'TRY', 'Otokar Otomotiv ve Savunma Sanayi A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (106, 'BIST:OYAKC', 'OYAKC', null, 'TRY', 'OYAK Çimento Fabrikaları A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (107, 'BIST:PASEU', 'PASEU', null, 'TRY', 'Pasifik Eurasia Lojistik Dış Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (108, 'BIST:PATEK', 'PATEK', null, 'TRY', 'Pasifik Teknoloji A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (109, 'BIST:PETKM', 'PETKM', null, 'TRY', 'Petkim Petrokimya Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (110, 'BIST:PGSUS', 'PGSUS', null, 'TRY', 'Pegasus Hava Taşımacılığı A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (111, 'BIST:QUAGR', 'QUAGR', null, 'TRY', 'Qua Granite Hayal Yapı ve Ürünleri Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (112, 'BIST:RALYH', 'RALYH', null, 'TRY', 'Ral Yatırım Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (113, 'BIST:REEDR', 'REEDR', null, 'TRY', 'Reeder Teknoloji Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (114, 'BIST:SAHOL', 'SAHOL', null, 'TRY', 'Hacı Ömer Sabancı Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (115, 'BIST:SASA', 'SASA', null, 'TRY', 'SASA Polyester Sanayi A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (116, 'BIST:SISE', 'SISE', null, 'TRY', 'Türkiye Şişe ve Cam Fabrikaları A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (117, 'BIST:SKBNK', 'SKBNK', null, 'TRY', 'Şekerbank T.A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (118, 'BIST:SOKM', 'SOKM', null, 'TRY', 'Şok Marketler Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (119, 'BIST:TABGD', 'TABGD', null, 'TRY', 'TAB Gıda Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (120, 'BIST:TAVHL', 'TAVHL', null, 'TRY', 'TAV Havalimanları Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (121, 'BIST:TCELL', 'TCELL', null, 'TRY', 'Turkcell İletişim Hizmetleri A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (122, 'BIST:THYAO', 'THYAO', null, 'TRY', 'Türk Hava Yolları A.O.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (123, 'BIST:TKFEN', 'TKFEN', null, 'TRY', 'Tekfen Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (124, 'BIST:TOASO', 'TOASO', null, 'TRY', 'Tofaş Türk Otomobil Fabrikası A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (125, 'BIST:TRALT', 'TRALT', null, 'TRY', 'Trakya Altın İşletmeleri A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (126, 'BIST:TRENJ', 'TRENJ', null, 'TRY', 'Tera Yatırım Teknoloji Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (127, 'BIST:TRMET', 'TRMET', null, 'TRY', 'Trakya Metal Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (128, 'BIST:TSKB', 'TSKB', null, 'TRY', 'Türkiye Sınai Kalkınma Bankası A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (129, 'BIST:TSPOR', 'TSPOR', null, 'TRY', 'Trabzonspor Sportif Yatırım ve Futbol İşletmeciliği Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (130, 'BIST:TTKOM', 'TTKOM', null, 'TRY', 'Türk Telekomünikasyon A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (131, 'BIST:TTRAK', 'TTRAK', null, 'TRY', 'Türk Traktör ve Ziraat Makineleri A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (132, 'BIST:TUKAS', 'TUKAS', null, 'TRY', 'Tukaş Gıda Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (133, 'BIST:TUPRS', 'TUPRS', null, 'TRY', 'Türkiye Petrol Rafinerileri A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (134, 'BIST:TUREX', 'TUREX', null, 'TRY', 'Turex Turizm Taşımacılık A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (135, 'BIST:TURSG', 'TURSG', null, 'TRY', 'Türkiye Sigorta A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (136, 'BIST:ULKER', 'ULKER', null, 'TRY', 'Ülker Bisküvi Sanayi A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (137, 'BIST:VAKBN', 'VAKBN', null, 'TRY', 'Türkiye Vakıflar Bankası T.A.O.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (138, 'BIST:VESTL', 'VESTL', null, 'TRY', 'Vestel Elektronik Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (139, 'BIST:YEOTK', 'YEOTK', null, 'TRY', 'YEO Teknoloji Enerji ve Endüstri A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (140, 'BIST:YKBNK', 'YKBNK', null, 'TRY', 'Yapı ve Kredi Bankası A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (141, 'BIST:ZOREN', 'ZOREN', null, 'TRY', 'Zorlu Enerji Elektrik Üretim A.Ş.', 4, 20, 1000, 1);
			