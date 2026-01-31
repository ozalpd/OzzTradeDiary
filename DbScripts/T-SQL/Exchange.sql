SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go
CREATE TABLE [dbo].[Exchanges](
    [Id] [int] Identity(1,1) Not Null,
    [ExchangeName] [nVarChar](48) Null,
    [ExchangeCode] [nVarChar](8) Null,
    [DisplayOrder] [int] Not Null Default 1000,
    [IsActive] [bit] Not Null Default 1,
  CONSTRAINT [PK_Exchanges] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_Exchanges_ExchangeName] On [dbo].[Exchanges]([ExchangeName] Asc)
Go
Create Nonclustered Index [idx_Exchanges_ExchangeCode] On [dbo].[Exchanges]([ExchangeCode] Asc)
Go
Create Nonclustered Index [idx_Exchanges_DisplayOrder] On [dbo].[Exchanges]([DisplayOrder] Asc)
Go
Create Nonclustered Index [idx_Exchanges_IsActive] On [dbo].[Exchanges]([IsActive] Asc)
Go
