SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go
CREATE TABLE [dbo].[EntryOrders](
    [Id] [int] Identity(1,1) Not Null,
    [TradeId] [int] Not Null Constraint FK_EntryOrder_TradeId Foreign Key References [dbo].[Trades]([Id]),
    [ExecuteTime] [DateTime] Null,
    [OrderPrice] [decimal](18, 4) Not Null, /* Planned Entry Price */
    [FilledPrice] [decimal](18, 4) Null, /* Executed Entry Price */
    [OrderQuantity] [decimal](18, 4) Null, /* Planned contract quantity of order */
    [FilledQuantity] [decimal](18, 4) Null, /* Realized contract quantity of order */
    [OrderAmount] [decimal](18, 4) Null, /* Planned amount in currency, like $100 */
    [FilledAmount] [decimal](18, 4) Null, /* Filled contract amount in currency, like $100 */
    [DisplayOrder] [int] Not Null Default 1000,
  CONSTRAINT [PK_EntryOrders] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_EntryOrders_TradeId] On [dbo].[EntryOrders]([TradeId] Asc)
Go
Create Nonclustered Index [idx_EntryOrders_ExecuteTime] On [dbo].[EntryOrders]([ExecuteTime] Asc)
Go
Create Nonclustered Index [idx_EntryOrders_DisplayOrder] On [dbo].[EntryOrders]([DisplayOrder] Asc)
Go

/****** Log table for EntryOrders  ******/
CREATE TABLE [log].[EntryOrdersLog](
    [Id] [int] Identity(1,1) Not Null,
    [EntryOrderId] [int],
    [TradeId] [int] Not Null Constraint FK_EntryOrdersLog_TradeId Foreign Key References [dbo].[Trades]([Id]),
    [ExecuteTime] [DateTime] Null,
    [OrderPrice] [decimal](18, 4) Not Null,
    [FilledPrice] [decimal](18, 4) Null,
    [OrderQuantity] [decimal](18, 4) Null,
    [FilledQuantity] [decimal](18, 4) Null,
    [OrderAmount] [decimal](18, 4) Null,
    [FilledAmount] [decimal](18, 4) Null,
    [DisplayOrder] [int] Not Null Default 1000,
    [LogStatus] [int] Not Null,
    [LogStatusDescription] [nVarChar](100) Not Null,
  CONSTRAINT [PK_EntryOrdersLog] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
