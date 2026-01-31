SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go
CREATE TABLE [dbo].[Symbols](
    [Id] [int] Identity(1,1) Not Null,
    [Ticker] [nVarChar](48) Not Null, /* Symbol name without exchange prefix, e.g. 'BTUSDT' */
    [TickerFull] [nVarChar](64) Not Null, /* Symbol name with exchange prefix, e.g. 'BYBIT:BTCUSD.P' */
    [BaseCurrency] [nVarChar](8) Null, /* Contains a string (CurrencyTicker) that representing the symbol's base currency if the instrument is a Crypto pair or a Forex pair or a derivative based on such a pair. Otherwise, it contains empty string. For example, this property holds "GBP" for "GBPJPY", "BTC" for "BTCUSDT" and "" for "NASDAQ:MSFT". */
    [PriceCurrency] [nVarChar](8) Not Null, /* Contains a string (CurrencyTicker) that representing currency of the symbol's price. For example, this property holds "JPY" for "GBPJPY", "USDT" for "BTCUSDT" and "USD" for "NASDAQ:MSFT". */
    [Description] [nVarChar](255) Null, /* Description of the symbol. */
    [ExchangeId] [int] Not Null Constraint FK_Symbol_ExchangeId Foreign Key References [dbo].[Exchanges]([Id]), /* The Id value of exchange record that related to the symbol. */
    [MarketType] [int] Not Null,
    [DisplayOrder] [int] Not Null Default 1000,
    [IsActive] [bit] Not Null Default 1,
  CONSTRAINT [PK_Symbols] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_Symbols_Ticker] On [dbo].[Symbols]([Ticker] Asc)
Go
Create Nonclustered Index [idx_Symbols_ExchangeId] On [dbo].[Symbols]([ExchangeId] Asc)
Go
Create Nonclustered Index [idx_Symbols_DisplayOrder] On [dbo].[Symbols]([DisplayOrder] Asc)
Go
Create Nonclustered Index [idx_Symbols_IsActive] On [dbo].[Symbols]([IsActive] Asc)
Go
