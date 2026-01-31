SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go
CREATE TABLE [dbo].[TradePlans](
    [Id] [int] Identity(1,1) Not Null,
    [TradeId] [int] Not Null Constraint FK_TradePlan_TradeId Foreign Key References [dbo].[Trades]([Id]),
    [ImageURL] [nVarChar](max) Not Null, /* Web URL or local file path of the image */
    [Notes] [nVarChar](max) Null,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
  CONSTRAINT [PK_TradePlans] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_TradePlans_TradeId] On [dbo].[TradePlans]([TradeId] Asc)
Go
Create Nonclustered Index [idx_TradePlans_ModifyDate] On [dbo].[TradePlans]([ModifyDate] Desc)
Go

/****** Log table for TradePlans  ******/
CREATE TABLE [log].[TradePlansLog](
    [Id] [int] Identity(1,1) Not Null,
    [TradePlanId] [int],
    [TradeId] [int] Not Null Constraint FK_TradePlansLog_TradeId Foreign Key References [dbo].[Trades]([Id]),
    [ImageURL] [nVarChar](max) Not Null,
    [Notes] [nVarChar](max) Null,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [LogStatus] [int] Not Null,
    [LogStatusDescription] [nVarChar](100) Not Null,
  CONSTRAINT [PK_TradePlansLog] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
