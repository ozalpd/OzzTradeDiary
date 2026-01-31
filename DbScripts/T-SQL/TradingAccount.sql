SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go
CREATE TABLE [dbo].[TradingAccounts](
    [Id] [int] Identity(1,1) Not Null,
    [Title] [nVarChar](128) Not Null,
    [AccountCode] [nVarChar](20) Null,
    [ExchangeId] [int] Not Null Constraint FK_TradingAccount_ExchangeId Foreign Key References [dbo].[Exchanges]([Id]), /* The Id value of exchange record that related to the account. */
    [Notes] [nVarChar](max) Null,
    [DisplayOrder] [int] Not Null Default 1000,
    [IsActive] [bit] Not Null Default 1,
  CONSTRAINT [PK_TradingAccounts] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_TradingAccounts_Title] On [dbo].[TradingAccounts]([Title] Asc)
Go
Create Nonclustered Index [idx_TradingAccounts_ExchangeId] On [dbo].[TradingAccounts]([ExchangeId] Asc)
Go
Create Nonclustered Index [idx_TradingAccounts_DisplayOrder] On [dbo].[TradingAccounts]([DisplayOrder] Asc)
Go
Create Nonclustered Index [idx_TradingAccounts_IsActive] On [dbo].[TradingAccounts]([IsActive] Asc)
Go
