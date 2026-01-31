SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go

-- --------------------------------------------------------------------
-- Stored Procedure: [log].[TradesLog_Insert]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[log].[TradesLog_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [log].[TradesLog_Insert]
Go
CREATE PROCEDURE [log].[TradesLog_Insert]
    @TradeId [int],
    @LogStatus [int],
    @LogStatusDescription [nVarChar](100)
As
Begin
    Insert Into [log].[TradesLog](
            [TradeId],
            [TradingAccountId],
            [SymbolId],
            [EntryTime],
            [EntryMethod],
            [TradeDirection],
            [PlannedEntry],
            [ExecutedEntry],
            [PlannedTP],
            [ExecutedTP],
            [PlannedSL],
            [ExecutedSL],
            [ModifyDate],
            [LogStatus], 
            [LogStatusDescription])
    Select  [Id],
            [TradingAccountId],
            [SymbolId],
            [EntryTime],
            [EntryMethod],
            [TradeDirection],
            [PlannedEntry],
            [ExecutedEntry],
            [PlannedTP],
            [ExecutedTP],
            [PlannedSL],
            [ExecutedSL],
            [ModifyDate],
            @LogStatus,
            @LogStatusDescription
    From  [dbo].[Trades]
    Where [dbo].[Trades].[Id] = @TradeId;
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[Trades_Delete]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Trades_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Trades_Delete]
Go
CREATE PROCEDURE [dbo].[Trades_Delete]
    @Id [int]
As
Begin
    /* Silinmeden önceki son hali loglanıyor... */
    Exec [log].[TradesLog_Insert]
        @TradeId = @Id,
        @LogStatus = 4010,
        @LogStatusDescription = N'Siliniyor...';

    /* TODO: Check cascaded data from other tables */
    Delete From [dbo].[Trades]
        Where [dbo].[Trades].[Id] = @Id
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[Trades_Insert]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Trades_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Trades_Insert]
Go
CREATE PROCEDURE [dbo].[Trades_Insert]
    @TradingAccountId [int], 
    @SymbolId [int], 
    @EntryMethod [int], 
    @TradeDirection [int], 
    @PlannedEntry [decimal](18, 4), 
    @ExecutedEntry [decimal](18, 4), 
    @PlannedTP [decimal](18, 4), 
    @ExecutedTP [decimal](18, 4), 
    @PlannedSL [decimal](18, 4), 
    @ExecutedSL [decimal](18, 4) 
As
Begin
    Declare @newKey        int

    Insert Into [dbo].[Trades](
            [TradingAccountId], [SymbolId], [EntryMethod], 
            [TradeDirection], [PlannedEntry], [ExecutedEntry], 
            [PlannedTP], [ExecutedTP], [PlannedSL], [ExecutedSL], 
            [ModifyDate])
    Values (@TradingAccountId, @SymbolId, @EntryMethod, 
            @TradeDirection, @PlannedEntry, @ExecutedEntry, 
            @PlannedTP, @ExecutedTP, @PlannedSL, @ExecutedSL, 
            GetDate())

    Set @newKey = SCOPE_IDENTITY();
    Exec [log].[TradesLog_Insert]
        @TradeId = @newKey,
        @LogStatus = 1010,
        @LogStatusDescription = N'Yeni Kayıt';

    Select  [Trades].[Id],
            [Trades].[TradingAccountId], 
            [Trades].[SymbolId], 
            [Trades].[EntryTime], 
            [Trades].[EntryMethod], 
            [Trades].[TradeDirection], 
            [Trades].[PlannedEntry], 
            [Trades].[ExecutedEntry], 
            [Trades].[PlannedTP], 
            [Trades].[ExecutedTP], 
            [Trades].[PlannedSL], 
            [Trades].[ExecutedSL], 
            [Trades].[ModifyDate] 
    From  [dbo].[Trades]
    Where [dbo].[Trades].[Id] = @newKey
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[Trades_Update]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Trades_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Trades_Update]
Go
CREATE PROCEDURE [dbo].[Trades_Update]
    @Id [int], 
    @EntryMethod [int], 
    @PlannedEntry [decimal](18, 4), 
    @ExecutedEntry [decimal](18, 4), 
    @PlannedTP [decimal](18, 4), 
    @ExecutedTP [decimal](18, 4), 
    @PlannedSL [decimal](18, 4), 
    @ExecutedSL [decimal](18, 4) 
As
Begin
    Update [dbo].[Trades]
       Set [EntryMethod] = @EntryMethod, 
           [PlannedEntry] = @PlannedEntry, 
           [ExecutedEntry] = @ExecutedEntry, 
           [PlannedTP] = @PlannedTP, 
           [ExecutedTP] = @ExecutedTP, 
           [PlannedSL] = @PlannedSL, 
           [ExecutedSL] = @ExecutedSL
     Where [dbo].[Trades].[Id] = @Id

    Exec [log].[TradesLog_Insert]
        @TradeId = @Id,
        @LogStatus = 1020,
        @LogStatusDescription = N'Genel Güncelleme';

    Select  [Trades].[Id],
            [Trades].[TradingAccountId], 
            [Trades].[SymbolId], 
            [Trades].[EntryTime], 
            [Trades].[EntryMethod], 
            [Trades].[TradeDirection], 
            [Trades].[PlannedEntry], 
            [Trades].[ExecutedEntry], 
            [Trades].[PlannedTP], 
            [Trades].[ExecutedTP], 
            [Trades].[PlannedSL], 
            [Trades].[ExecutedSL], 
            [Trades].[ModifyDate] 
    From  [dbo].[Trades]
    Where [dbo].[Trades].[Id] = @Id
End
Go
