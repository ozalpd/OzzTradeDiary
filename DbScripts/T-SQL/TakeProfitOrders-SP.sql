SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go

-- --------------------------------------------------------------------
-- Stored Procedure: [log].[TakeProfitOrdersLog_Insert]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[log].[TakeProfitOrdersLog_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [log].[TakeProfitOrdersLog_Insert]
Go
CREATE PROCEDURE [log].[TakeProfitOrdersLog_Insert]
    @TakeProfitOrderId [int],
    @LogStatus [int],
    @LogStatusDescription [nVarChar](100)
As
Begin
    Insert Into [log].[TakeProfitOrdersLog](
            [TakeProfitOrderId],
            [TradeId],
            [ExecuteTime],
            [OrderPrice],
            [FilledPrice],
            [OrderQuantity],
            [FilledQuantity],
            [OrderAmount],
            [FilledAmount],
            [DisplayOrder],
            [LogStatus], 
            [LogStatusDescription])
    Select  [Id],
            [TradeId],
            [ExecuteTime],
            [OrderPrice],
            [FilledPrice],
            [OrderQuantity],
            [FilledQuantity],
            [OrderAmount],
            [FilledAmount],
            [DisplayOrder],
            @LogStatus,
            @LogStatusDescription
    From  [dbo].[TakeProfitOrders]
    Where [dbo].[TakeProfitOrders].[Id] = @TakeProfitOrderId;
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[TakeProfitOrders_Delete]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TakeProfitOrders_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TakeProfitOrders_Delete]
Go
CREATE PROCEDURE [dbo].[TakeProfitOrders_Delete]
    @Id [int]
As
Begin
    /* Silinmeden önceki son hali loglanıyor... */
    Exec [log].[TakeProfitOrdersLog_Insert]
        @TakeProfitOrderId = @Id,
        @LogStatus = 4010,
        @LogStatusDescription = N'Siliniyor...';

    /* TODO: Check cascaded data from other tables */
    Delete From [dbo].[TakeProfitOrders]
        Where [dbo].[TakeProfitOrders].[Id] = @Id
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[TakeProfitOrders_Insert]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TakeProfitOrders_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TakeProfitOrders_Insert]
Go
CREATE PROCEDURE [dbo].[TakeProfitOrders_Insert]
    @TradeId [int], 
    @OrderPrice [decimal](18, 4), 
    @FilledPrice [decimal](18, 4), 
    @OrderQuantity [decimal](18, 4), 
    @FilledQuantity [decimal](18, 4), 
    @OrderAmount [decimal](18, 4), 
    @FilledAmount [decimal](18, 4) 
As
Begin
    Declare @newKey        int

    Insert Into [dbo].[TakeProfitOrders](
            [TradeId], [OrderPrice], [FilledPrice], [OrderQuantity], 
            [FilledQuantity], [OrderAmount], [FilledAmount], 
            [DisplayOrder])
    Values (@TradeId, @OrderPrice, @FilledPrice, @OrderQuantity, 
            @FilledQuantity, @OrderAmount, @FilledAmount, 
            1000)

    Set @newKey = SCOPE_IDENTITY();
    Exec [log].[TakeProfitOrdersLog_Insert]
        @TakeProfitOrderId = @newKey,
        @LogStatus = 1010,
        @LogStatusDescription = N'Yeni Kayıt';

    Select  [TakeProfitOrders].[Id],
            [TakeProfitOrders].[TradeId], 
            [TakeProfitOrders].[ExecuteTime], 
            [TakeProfitOrders].[OrderPrice], 
            [TakeProfitOrders].[FilledPrice], 
            [TakeProfitOrders].[OrderQuantity], 
            [TakeProfitOrders].[FilledQuantity], 
            [TakeProfitOrders].[OrderAmount], 
            [TakeProfitOrders].[FilledAmount], 
            [TakeProfitOrders].[DisplayOrder] 
    From  [dbo].[TakeProfitOrders]
    Where [dbo].[TakeProfitOrders].[Id] = @newKey
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[TakeProfitOrders_Update]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TakeProfitOrders_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TakeProfitOrders_Update]
Go
CREATE PROCEDURE [dbo].[TakeProfitOrders_Update]
    @Id [int], 
    @ExecuteTime [DateTime], 
    @OrderPrice [decimal](18, 4), 
    @FilledPrice [decimal](18, 4), 
    @OrderQuantity [decimal](18, 4), 
    @FilledQuantity [decimal](18, 4), 
    @OrderAmount [decimal](18, 4), 
    @FilledAmount [decimal](18, 4) 
As
Begin
    Update [dbo].[TakeProfitOrders]
       Set [ExecuteTime] = @ExecuteTime, 
           [OrderPrice] = @OrderPrice, 
           [FilledPrice] = @FilledPrice, 
           [OrderQuantity] = @OrderQuantity, 
           [FilledQuantity] = @FilledQuantity, 
           [OrderAmount] = @OrderAmount, 
           [FilledAmount] = @FilledAmount
     Where [dbo].[TakeProfitOrders].[Id] = @Id

    Exec [log].[TakeProfitOrdersLog_Insert]
        @TakeProfitOrderId = @Id,
        @LogStatus = 1020,
        @LogStatusDescription = N'Genel Güncelleme';

    Select  [TakeProfitOrders].[Id],
            [TakeProfitOrders].[TradeId], 
            [TakeProfitOrders].[ExecuteTime], 
            [TakeProfitOrders].[OrderPrice], 
            [TakeProfitOrders].[FilledPrice], 
            [TakeProfitOrders].[OrderQuantity], 
            [TakeProfitOrders].[FilledQuantity], 
            [TakeProfitOrders].[OrderAmount], 
            [TakeProfitOrders].[FilledAmount], 
            [TakeProfitOrders].[DisplayOrder] 
    From  [dbo].[TakeProfitOrders]
    Where [dbo].[TakeProfitOrders].[Id] = @Id
End
Go
