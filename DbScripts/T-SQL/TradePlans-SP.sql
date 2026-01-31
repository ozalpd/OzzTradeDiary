SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go

-- --------------------------------------------------------------------
-- Stored Procedure: [log].[TradePlansLog_Insert]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[log].[TradePlansLog_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [log].[TradePlansLog_Insert]
Go
CREATE PROCEDURE [log].[TradePlansLog_Insert]
    @TradePlanId [int],
    @LogStatus [int],
    @LogStatusDescription [nVarChar](100)
As
Begin
    Insert Into [log].[TradePlansLog](
            [TradePlanId],
            [TradeId],
            [ImageURL],
            [Notes],
            [ModifyDate],
            [LogStatus], 
            [LogStatusDescription])
    Select  [Id],
            [TradeId],
            [ImageURL],
            [Notes],
            [ModifyDate],
            @LogStatus,
            @LogStatusDescription
    From  [dbo].[TradePlans]
    Where [dbo].[TradePlans].[Id] = @TradePlanId;
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[TradePlans_Delete]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TradePlans_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TradePlans_Delete]
Go
CREATE PROCEDURE [dbo].[TradePlans_Delete]
    @Id [int]
As
Begin
    /* Silinmeden önceki son hali loglanıyor... */
    Exec [log].[TradePlansLog_Insert]
        @TradePlanId = @Id,
        @LogStatus = 4010,
        @LogStatusDescription = N'Siliniyor...';

    /* TODO: Check cascaded data from other tables */
    Delete From [dbo].[TradePlans]
        Where [dbo].[TradePlans].[Id] = @Id
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[TradePlans_Insert]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TradePlans_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TradePlans_Insert]
Go
CREATE PROCEDURE [dbo].[TradePlans_Insert]
    @TradeId [int], 
    @ImageURL [nVarChar](max), 
    @Notes [nVarChar](max) 
As
Begin
    Declare @newKey        int

    Insert Into [dbo].[TradePlans](
            [TradeId], [ImageURL], [Notes], [ModifyDate])
    Values (@TradeId, @ImageURL, @Notes, GetDate())

    Set @newKey = SCOPE_IDENTITY();
    Exec [log].[TradePlansLog_Insert]
        @TradePlanId = @newKey,
        @LogStatus = 1010,
        @LogStatusDescription = N'Yeni Kayıt';

    Select  [TradePlans].[Id],
            [TradePlans].[TradeId], 
            [TradePlans].[ImageURL], 
            [TradePlans].[Notes], 
            [TradePlans].[ModifyDate] 
    From  [dbo].[TradePlans]
    Where [dbo].[TradePlans].[Id] = @newKey
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[TradePlans_Update]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TradePlans_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TradePlans_Update]
Go
CREATE PROCEDURE [dbo].[TradePlans_Update]
    @Id [int], 
    @ImageURL [nVarChar](max), 
    @Notes [nVarChar](max) 
As
Begin
    Update [dbo].[TradePlans]
       Set [ImageURL] = @ImageURL, [Notes] = @Notes
     Where [dbo].[TradePlans].[Id] = @Id

    Exec [log].[TradePlansLog_Insert]
        @TradePlanId = @Id,
        @LogStatus = 1020,
        @LogStatusDescription = N'Genel Güncelleme';

    Select  [TradePlans].[Id],
            [TradePlans].[TradeId], 
            [TradePlans].[ImageURL], 
            [TradePlans].[Notes], 
            [TradePlans].[ModifyDate] 
    From  [dbo].[TradePlans]
    Where [dbo].[TradePlans].[Id] = @Id
End
Go
