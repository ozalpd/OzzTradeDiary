SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go
CREATE TABLE [dbo].[Currencies](
    [Id] [int] Identity(1,1) Not Null,
    [CurrencyTicker] [nVarChar](8) Not Null, /* Ticker or short code of the currency */
    [Description] [nVarChar](255) Null, /* Description for the currency. */
    [DisplayOrder] [int] Not Null Default 1000,
    [IsActive] [bit] Not Null Default 1,
  CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_Currencies_CurrencyTicker] On [dbo].[Currencies]([CurrencyTicker] Asc)
Go
Create Nonclustered Index [idx_Currencies_DisplayOrder] On [dbo].[Currencies]([DisplayOrder] Asc)
Go
Create Nonclustered Index [idx_Currencies_IsActive] On [dbo].[Currencies]([IsActive] Asc)
Go
