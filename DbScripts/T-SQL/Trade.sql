SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go
CREATE TABLE [dbo].[Trades](
    [Id] [int] Identity(1,1) Not Null,
    [TradingAccountId] [int] Not Null Constraint FK_Trade_TradingAccountId Foreign Key References [dbo].[TradingAccounts]([Id]),
    [SymbolId] [int] Not Null Constraint FK_Trade_SymbolId Foreign Key References [dbo].[Symbols]([Id]),
    [EntryTime] [DateTime] Null,
    [EntryMethod] [int] Not Null,
    [TradeDirection] [int] Not Null,
    [PlannedEntry] [decimal](18, 4) Null, /* Planned Entry Price, calculated from EntryOrders */
    [ExecutedEntry] [decimal](18, 4) Null, /* Executed Entry Price, calculated from EntryOrders */
    [PlannedTP] [decimal](18, 4) Null, /* Planned Take Profit Price, calculated from TakeProfitOrders */
    [ExecutedTP] [decimal](18, 4) Null, /* Executed Take Profit Price, calculated from TakeProfitOrders */
    [PlannedSL] [decimal](18, 4) Null, /* Planned Stop Loss Price, calculated from StopLossOrders */
    [ExecutedSL] [decimal](18, 4) Null, /* Executed Stop Loss Price, calculated from StopLossOrders */
    [ModifyDate] [DateTime] Not Null Default GetDate(),
  CONSTRAINT [PK_Trades] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_Trades_ModifyDate] On [dbo].[Trades]([ModifyDate] Desc)
Go

/****** Log table for Trades  ******/
CREATE TABLE [log].[TradesLog](
    [Id] [int] Identity(1,1) Not Null,
    [TradeId] [int],
    [TradingAccountId] [int] Not Null Constraint FK_TradesLog_TradingAccountId Foreign Key References [dbo].[TradingAccounts]([Id]),
    [SymbolId] [int] Not Null Constraint FK_TradesLog_SymbolId Foreign Key References [dbo].[Symbols]([Id]),
    [EntryTime] [DateTime] Null,
    [EntryMethod] [int] Not Null,
    [TradeDirection] [int] Not Null,
    [PlannedEntry] [decimal](18, 4) Null,
    [ExecutedEntry] [decimal](18, 4) Null,
    [PlannedTP] [decimal](18, 4) Null,
    [ExecutedTP] [decimal](18, 4) Null,
    [PlannedSL] [decimal](18, 4) Null,
    [ExecutedSL] [decimal](18, 4) Null,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [LogStatus] [int] Not Null,
    [LogStatusDescription] [nVarChar](100) Not Null,
  CONSTRAINT [PK_TradesLog] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
