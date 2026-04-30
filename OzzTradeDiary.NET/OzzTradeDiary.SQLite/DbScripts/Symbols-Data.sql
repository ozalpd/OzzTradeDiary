/* TickerFull Symbol name with exchange prefix, e.g. 'BYBIT:BTCUSD.P'
   Ticker Symbol name without exchange prefix, e.g. 'BTCUSD.P'
   BaseCurrency The base currency of the symbol, e.g. 'BTC' for 'BTCUSD.P' but empty string for stocks eg 'NASDAQ:AAPL'
   PriceCurrencyId The Id of price currency of the symbol, e.g. 2 for 'BTCUSDT.P', 13 for 'GBPJPY', 1 for 'NASDAQ:AAPL' other currencies for different stock markets
   ExchangeId The ID of the exchange where the symbol is listed, e.g. 1 for 'NYSE', 2 for 'NASDAQ', 6 for BYBIT, 5 for BINGX, 21 for BINANCE, 4 for BIST etc
   MarketType The type of market, its an enum at C# side, 20 for 'Stock', 50 for 'Forex', 80 for 'Crypto', 90 for 'CryptoPerpetual'
   DisplayOrder The order in which the symbol is displayed
   IsActive Indicates if the symbol is active (1) or inactive (0)
*/
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (1, 'NASDAQ:AAPL', 'AAPL', null, 1, 'Apple Inc.', 2, 20, 1000, 1);

Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (2, 'BYBIT:BTCUSDT', 'BTCUSDT', 'BTC', 2, 'Bitcoin Spot', 6, 80, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (3, 'BYBIT:BTCUSDC', 'BTCUSDC', 'BTC', 3, 'Bitcoin Spot', 6, 80, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (4, 'BYBIT:BTCUSDT.P', 'BTCUSDT.P', 'BTC', 2, 'Bitcoin Perpetual Contract', 6, 90, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (5, 'BYBIT:BTCUSDC.P', 'BTCUSDC.P', 'BTC', 3, 'Bitcoin Perpetual Contract', 6, 90, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (6, 'BYBIT:ETHUSDT', 'ETHUSDT', 'ETH', 2, 'Ethereum Spot', 6, 80, 200, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (7, 'BYBIT:ETHUSDC', 'ETHUSDC', 'ETH', 3, 'Ethereum Spot', 6, 80, 200, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (8, 'BYBIT:ETHUSDT.P', 'ETHUSDT.P', 'ETH', 2, 'Ethereum Perpetual Contract', 6, 90, 200, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (9, 'BYBIT:ETHUSDC.P', 'ETHUSDC.P', 'ETH', 3, 'Ethereum Perpetual Contract', 6, 90, 200, 1);

Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (10, 'BINGX:BTCUSDT', 'BTCUSDT', 'BTC', 2, 'Bitcoin Spot', 5, 80, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (11, 'BINGX:BTCUSDC', 'BTCUSDC', 'BTC', 3, 'Bitcoin Spot', 5, 80, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (12, 'BINGX:BTCUSDT.P', 'BTCUSDT.P', 'BTC', 2, 'Bitcoin Perpetual Contract', 5, 90, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (13, 'BINGX:BTCUSDC.P', 'BTCUSDC.P', 'BTC', 3, 'Bitcoin Perpetual Contract', 5, 90, 100, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (14, 'BINGX:ETHUSDT', 'ETHUSDT', 'ETH', 2, 'Ethereum Spot', 5, 80, 200, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (15, 'BINGX:ETHUSDC', 'ETHUSDC', 'ETH', 3, 'Ethereum Spot', 5, 80, 200, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (16, 'BINGX:ETHUSDT.P', 'ETHUSDT.P', 'ETH', 2, 'Ethereum Perpetual Contract', 5, 90, 200, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (17, 'BINGX:ETHUSDC.P', 'ETHUSDC.P', 'ETH', 3, 'Ethereum Perpetual Contract', 5, 90, 200, 1);

Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (18, 'BYBIT:AVAXUSDT', 'AVAXUSDT', 'AVAX', 2, 'Avalanche Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (19, 'BYBIT:AVAXUSDC', 'AVAXUSDC', 'AVAX', 3, 'Avalanche Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (20, 'BYBIT:AVAXUSDT.P', 'AVAXUSDT.P', 'AVAX', 2, 'Avalanche Perpetual Contract', 6, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (21, 'BYBIT:AVAXUSDC.P', 'AVAXUSDC.P', 'AVAX', 3, 'Avalanche Perpetual Contract', 6, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (22, 'BYBIT:SOLUSDT', 'SOLUSDT', 'SOL', 2, 'Solana Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (23, 'BYBIT:SOLUSDC', 'SOLUSDC', 'SOL', 3, 'Solana Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (24, 'BYBIT:SOLUSDT.P', 'SOLUSDT.P', 'SOL', 2, 'Solana Perpetual Contract', 6, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (25, 'BYBIT:SOLUSDC.P', 'SOLUSDC.P', 'SOL', 3, 'Solana Perpetual Contract', 6, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (26, 'BYBIT:XRPUSDT', 'XRPUSDT', 'XRP', 2, 'XRP Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (27, 'BYBIT:XRPUSDC', 'XRPUSDC', 'XRP', 3, 'XRP Spot', 6, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (28, 'BYBIT:XRPUSDT.P', 'XRPUSDT.P', 'XRP', 2, 'XRP Perpetual Contract', 6, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (29, 'BYBIT:XRPUSDC.P', 'XRPUSDC.P', 'XRP', 3, 'XRP Perpetual Contract', 6, 90, 1000, 1);

Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (30, 'BINGX:AVAXUSDT', 'AVAXUSDT', 'AVAX', 2, 'Avalanche Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (31, 'BINGX:AVAXUSDC', 'AVAXUSDC', 'AVAX', 3, 'Avalanche Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (32, 'BINGX:AVAXUSDT.P', 'AVAXUSDT.P', 'AVAX', 2, 'Avalanche Perpetual Contract', 5, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (33, 'BINGX:AVAXUSDC.P', 'AVAXUSDC.P', 'AVAX', 3, 'Avalanche Perpetual Contract', 5, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (34, 'BINGX:SOLUSDT', 'SOLUSDT', 'SOL', 2, 'Solana Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (35, 'BINGX:SOLUSDC', 'SOLUSDC', 'SOL', 3, 'Solana Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (36, 'BINGX:SOLUSDT.P', 'SOLUSDT.P', 'SOL', 2, 'Solana Perpetual Contract', 5, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (37, 'BINGX:SOLUSDC.P', 'SOLUSDC.P', 'SOL', 3, 'Solana Perpetual Contract', 5, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (38, 'BINGX:XRPUSDT', 'XRPUSDT', 'XRP', 2, 'XRP Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (39, 'BINGX:XRPUSDC', 'XRPUSDC', 'XRP', 3, 'XRP Spot', 5, 80, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (40, 'BINGX:XRPUSDT.P', 'XRPUSDT.P', 'XRP', 2, 'XRP Perpetual Contract', 5, 90, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (41, 'BINGX:XRPUSDC.P', 'XRPUSDC.P', 'XRP', 3, 'XRP Perpetual Contract', 5, 90, 1000, 1);

Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (42, 'BIST:AEFES', 'AEFES', null, 21, 'Anadolu Efes Biracılık ve Malt Sanayii A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (43, 'BIST:AGHOL', 'AGHOL', null, 21, 'Anadolu Grubu Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (44, 'BIST:AKBNK', 'AKBNK', null, 21, 'Akbank T.A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (45, 'BIST:AKSA', 'AKSA', null, 21, 'Aksa Akrilik Kimya Sanayii A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (46, 'BIST:AKSEN', 'AKSEN', null, 21, 'Aksa Enerji Üretim A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (47, 'BIST:ALARK', 'ALARK', null, 21, 'Alarko Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (48, 'BIST:ALTNY', 'ALTNY', null, 21, 'Altınay Savunma Teknolojileri A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (49, 'BIST:ANSGR', 'ANSGR', null, 21, 'Anadolu Sigorta A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (50, 'BIST:ARCLK', 'ARCLK', null, 21, 'Arçelik A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (51, 'BIST:ASELS', 'ASELS', null, 21, 'Aselsan Elektronik Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (52, 'BIST:ASTOR', 'ASTOR', null, 21, 'Astor Enerji A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (53, 'BIST:BALSU', 'BALSU', null, 21, 'Balsu Gıda Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (54, 'BIST:BIMAS', 'BIMAS', null, 21, 'BİM Birleşik Mağazalar A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (55, 'BIST:BRSAN', 'BRSAN', null, 21, 'Borusan Boru Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (56, 'BIST:BRYAT', 'BRYAT', null, 21, 'Borusan Yatırım ve Pazarlama A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (57, 'BIST:BSOKE', 'BSOKE', null, 21, 'Batısöke Söke Çimento Sanayii T.A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (58, 'BIST:BTCIM', 'BTCIM', null, 21, 'Batıçim Batı Anadolu Çimento Sanayii A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (59, 'BIST:CANTE', 'CANTE', null, 21, 'Çan2 Termik A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (60, 'BIST:CCOLA', 'CCOLA', null, 21, 'Coca-Cola İçecek A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (61, 'BIST:CIMSA', 'CIMSA', null, 21, 'Çimsa Çimento Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (62, 'BIST:CWENE', 'CWENE', null, 21, 'CW Enerji Mühendislik Ticaret ve Sanayi A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (63, 'BIST:DAPGM', 'DAPGM', null, 21, 'DAP Gayrimenkul Geliştirme A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (64, 'BIST:DOAS', 'DOAS', null, 21, 'Doğuş Otomotiv Servis ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (65, 'BIST:DOHOL', 'DOHOL', null, 21, 'Doğan Şirketler Grubu Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (66, 'BIST:DSTKF', 'DSTKF', null, 21, 'Destek Finans Faktoring A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (67, 'BIST:ECILC', 'ECILC', null, 21, 'EİS Eczacıbaşı İlaç, Sınai ve Finansal Yatırımlar Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (68, 'BIST:EFOR', 'EFOR', null, 21, 'Efor Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (69, 'BIST:EGEEN', 'EGEEN', null, 21, 'Ege Endüstri ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (70, 'BIST:EKGYO', 'EKGYO', null, 21, 'Emlak Konut Gayrimenkul Yatırım Ortaklığı A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (71, 'BIST:ENERY', 'ENERY', null, 21, 'Enerya Enerji A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (72, 'BIST:ENJSA', 'ENJSA', null, 21, 'Enerjisa Enerji A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (73, 'BIST:ENKAI', 'ENKAI', null, 21, 'Enka İnşaat ve Sanayi A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (74, 'BIST:EREGL', 'EREGL', null, 21, 'Ereğli Demir ve Çelik Fabrikaları T.A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (75, 'BIST:EUPWR', 'EUPWR', null, 21, 'Europower Enerji ve Otomasyon Teknolojileri Sanayi Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (76, 'BIST:FENER', 'FENER', null, 21, 'Fenerbahçe Futbol A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (77, 'BIST:FROTO', 'FROTO', null, 21, 'Ford Otomotiv Sanayi A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (78, 'BIST:GARAN', 'GARAN', null, 21, 'Türkiye Garanti Bankası A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (79, 'BIST:GENIL', 'GENIL', null, 21, 'Gen İlaç ve Sağlık Ürünleri Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (80, 'BIST:GESAN', 'GESAN', null, 21, 'Girişim Elektrik Sanayi Taahhüt ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (81, 'BIST:GLRMK', 'GLRMK', null, 21, 'Gülermak Ağır Sanayi İnşaat ve Taahhüt A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (82, 'BIST:GRSEL', 'GRSEL', null, 21, 'Gür-Sel Turizm Taşımacılık ve Servis Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (83, 'BIST:GRTHO', 'GRTHO', null, 21, 'Graintürk Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (84, 'BIST:GSRAY', 'GSRAY', null, 21, 'Galatasaray Sportif Sınai ve Ticari Yatırımlar A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (85, 'BIST:GUBRF', 'GUBRF', null, 21, 'Gübre Fabrikaları T.A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (86, 'BIST:HALKB', 'HALKB', null, 21, 'Türkiye Halk Bankası A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (87, 'BIST:HEKTS', 'HEKTS', null, 21, 'Hektaş Ticaret T.A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (88, 'BIST:ISCTR', 'ISCTR', null, 21, 'Türkiye İş Bankası A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (89, 'BIST:ISMEN', 'ISMEN', null, 21, 'İş Yatırım Menkul Değerler A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (90, 'BIST:IZENR', 'IZENR', null, 21, 'İzdemir Enerji Elektrik Üretim A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (91, 'BIST:KCAER', 'KCAER', null, 21, 'Kocaer Çelik Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (92, 'BIST:KCHOL', 'KCHOL', null, 21, 'Koç Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (93, 'BIST:KLRHO', 'KLRHO', null, 21, 'Kiler Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (94, 'BIST:KONTR', 'KONTR', null, 21, 'Kontrolmatik Teknoloji Enerji ve Mühendislik A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (95, 'BIST:KRDMD', 'KRDMD', null, 21, 'Kardemir Karabük Demir Çelik Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (96, 'BIST:KTLEV', 'KTLEV', null, 21, 'Katılımevim Tasarruf Finansman A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (97, 'BIST:KUYAS', 'KUYAS', null, 21, 'Kuyas Yatırım A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (98, 'BIST:MAGEN', 'MAGEN', null, 21, 'Margün Enerji Üretim Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (99, 'BIST:MAVI', 'MAVI', null, 21, 'Mavi Giyim Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (100, 'BIST:MGROS', 'MGROS', null, 21, 'Migros Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (101, 'BIST:MIATK', 'MIATK', null, 21, 'Mia Teknoloji A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (102, 'BIST:MPARK', 'MPARK', null, 21, 'MLP Sağlık Hizmetleri A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (103, 'BIST:OBAMS', 'OBAMS', null, 21, 'Oba Makarnacılık Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (104, 'BIST:ODAS', 'ODAS', null, 21, 'Odaş Elektrik Üretim Sanayi Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (105, 'BIST:OTKAR', 'OTKAR', null, 21, 'Otokar Otomotiv ve Savunma Sanayi A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (106, 'BIST:OYAKC', 'OYAKC', null, 21, 'OYAK Çimento Fabrikaları A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (107, 'BIST:PASEU', 'PASEU', null, 21, 'Pasifik Eurasia Lojistik Dış Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (108, 'BIST:PATEK', 'PATEK', null, 21, 'Pasifik Teknoloji A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (109, 'BIST:PETKM', 'PETKM', null, 21, 'Petkim Petrokimya Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (110, 'BIST:PGSUS', 'PGSUS', null, 21, 'Pegasus Hava Taşımacılığı A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (111, 'BIST:QUAGR', 'QUAGR', null, 21, 'Qua Granite Hayal Yapı ve Ürünleri Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (112, 'BIST:RALYH', 'RALYH', null, 21, 'Ral Yatırım Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (113, 'BIST:REEDR', 'REEDR', null, 21, 'Reeder Teknoloji Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (114, 'BIST:SAHOL', 'SAHOL', null, 21, 'Hacı Ömer Sabancı Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (115, 'BIST:SASA', 'SASA', null, 21, 'SASA Polyester Sanayi A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (116, 'BIST:SISE', 'SISE', null, 21, 'Türkiye Şişe ve Cam Fabrikaları A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (117, 'BIST:SKBNK', 'SKBNK', null, 21, 'Şekerbank T.A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (118, 'BIST:SOKM', 'SOKM', null, 21, 'Şok Marketler Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (119, 'BIST:TABGD', 'TABGD', null, 21, 'TAB Gıda Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (120, 'BIST:TAVHL', 'TAVHL', null, 21, 'TAV Havalimanları Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (121, 'BIST:TCELL', 'TCELL', null, 21, 'Turkcell İletişim Hizmetleri A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (122, 'BIST:THYAO', 'THYAO', null, 21, 'Türk Hava Yolları A.O.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (123, 'BIST:TKFEN', 'TKFEN', null, 21, 'Tekfen Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (124, 'BIST:TOASO', 'TOASO', null, 21, 'Tofaş Türk Otomobil Fabrikası A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (125, 'BIST:TRALT', 'TRALT', null, 21, 'Trakya Altın İşletmeleri A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (126, 'BIST:TRENJ', 'TRENJ', null, 21, 'Tera Yatırım Teknoloji Holding A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (127, 'BIST:TRMET', 'TRMET', null, 21, 'Trakya Metal Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (128, 'BIST:TSKB', 'TSKB', null, 21, 'Türkiye Sınai Kalkınma Bankası A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (129, 'BIST:TSPOR', 'TSPOR', null, 21, 'Trabzonspor Sportif Yatırım ve Futbol İşletmeciliği Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (130, 'BIST:TTKOM', 'TTKOM', null, 21, 'Türk Telekomünikasyon A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (131, 'BIST:TTRAK', 'TTRAK', null, 21, 'Türk Traktör ve Ziraat Makineleri A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (132, 'BIST:TUKAS', 'TUKAS', null, 21, 'Tukaş Gıda Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (133, 'BIST:TUPRS', 'TUPRS', null, 21, 'Türkiye Petrol Rafinerileri A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (134, 'BIST:TUREX', 'TUREX', null, 21, 'Turex Turizm Taşımacılık A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (135, 'BIST:TURSG', 'TURSG', null, 21, 'Türkiye Sigorta A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (136, 'BIST:ULKER', 'ULKER', null, 21, 'Ülker Bisküvi Sanayi A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (137, 'BIST:VAKBN', 'VAKBN', null, 21, 'Türkiye Vakıflar Bankası T.A.O.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (138, 'BIST:VESTL', 'VESTL', null, 21, 'Vestel Elektronik Sanayi ve Ticaret A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (139, 'BIST:YEOTK', 'YEOTK', null, 21, 'YEO Teknoloji Enerji ve Endüstri A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (140, 'BIST:YKBNK', 'YKBNK', null, 21, 'Yapı ve Kredi Bankası A.Ş.', 4, 20, 1000, 1);
Insert Into Symbols(Id, TickerFull, Ticker, BaseCurrency, PriceCurrencyId, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
			values (141, 'BIST:ZOREN', 'ZOREN', null, 21, 'Zorlu Enerji Elektrik Üretim A.Ş.', 4, 20, 1000, 1);
			