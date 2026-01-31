SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go

-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[Symbols_Delete]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Symbols_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Symbols_Delete]
Go
CREATE PROCEDURE [dbo].[Symbols_Delete]
    @Id [int]
As
Begin
    /* TODO: Check cascaded data from other tables */
    Delete From [dbo].[Symbols]
        Where [dbo].[Symbols].[Id] = @Id
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[Symbols_Insert]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Symbols_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Symbols_Insert]
Go
CREATE PROCEDURE [dbo].[Symbols_Insert]
    @Ticker [nVarChar](48), 
    @TickerFull [nVarChar](64), 
    @BaseCurrency [nVarChar](8), 
    @PriceCurrency [nVarChar](8), 
    @Description [nVarChar](255), 
    @ExchangeId [int], 
    @MarketType [int] 
As
Begin
    Declare @newKey        int

    Insert Into [dbo].[Symbols](
            [Ticker], [TickerFull], [BaseCurrency], [PriceCurrency], 
            [Description], [ExchangeId], [MarketType], [DisplayOrder], 
            [IsActive])
    Values (@Ticker, @TickerFull, @BaseCurrency, @PriceCurrency, 
            @Description, @ExchangeId, @MarketType, 1000, 
            1)

    Set @newKey = SCOPE_IDENTITY();

    Select  [Symbols].[Id],
            [Symbols].[Ticker], 
            [Symbols].[TickerFull], 
            [Symbols].[BaseCurrency], 
            [Symbols].[PriceCurrency], 
            [Symbols].[Description], 
            [Symbols].[ExchangeId], 
            [Symbols].[MarketType], 
            [Symbols].[DisplayOrder], 
            [Symbols].[IsActive] 
    From  [dbo].[Symbols]
    Where [dbo].[Symbols].[Id] = @newKey
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[Symbols_Update]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Symbols_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Symbols_Update]
Go
CREATE PROCEDURE [dbo].[Symbols_Update]
    @Id [int], 
    @TickerFull [nVarChar](64), 
    @BaseCurrency [nVarChar](8), 
    @PriceCurrency [nVarChar](8), 
    @Description [nVarChar](255), 
    @MarketType [int] 
As
Begin
    Update [dbo].[Symbols]
       Set [TickerFull] = @TickerFull, 
           [BaseCurrency] = @BaseCurrency, 
           [PriceCurrency] = @PriceCurrency, 
           [Description] = @Description, 
           [MarketType] = @MarketType
     Where [dbo].[Symbols].[Id] = @Id

    Select  [Symbols].[Id],
            [Symbols].[Ticker], 
            [Symbols].[TickerFull], 
            [Symbols].[BaseCurrency], 
            [Symbols].[PriceCurrency], 
            [Symbols].[Description], 
            [Symbols].[ExchangeId], 
            [Symbols].[MarketType], 
            [Symbols].[DisplayOrder], 
            [Symbols].[IsActive] 
    From  [dbo].[Symbols]
    Where [dbo].[Symbols].[Id] = @Id
End
Go
