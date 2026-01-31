SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go

-- --------------------------------------------------------------------
-- Stored Procedure: [log].[StopLossOrdersLog_Insert]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[log].[StopLossOrdersLog_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [log].[StopLossOrdersLog_Insert]
Go
CREATE PROCEDURE [log].[StopLossOrdersLog_Insert]
    @StopLossOrderId [int],
    @LogStatus [int],
    @LogStatusDescription [nVarChar](100)
As
Begin
    Insert Into [log].[StopLossOrdersLog](
            [StopLossOrderId],
            [TradeId],
            [StopAll],
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
            [StopAll],
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
    From  [dbo].[StopLossOrders]
    Where [dbo].[StopLossOrders].[Id] = @StopLossOrderId;
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[StopLossOrders_Delete]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StopLossOrders_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[StopLossOrders_Delete]
Go
CREATE PROCEDURE [dbo].[StopLossOrders_Delete]
    @Id [int]
As
Begin
    /* Silinmeden önceki son hali loglanıyor... */
    Exec [log].[StopLossOrdersLog_Insert]
        @StopLossOrderId = @Id,
        @LogStatus = 4010,
        @LogStatusDescription = N'Siliniyor...';

    /* TODO: Check cascaded data from other tables */
    Delete From [dbo].[StopLossOrders]
        Where [dbo].[StopLossOrders].[Id] = @Id
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[StopLossOrders_Insert]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StopLossOrders_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[StopLossOrders_Insert]
Go
CREATE PROCEDURE [dbo].[StopLossOrders_Insert]
    @TradeId [int], 
    @StopAll [bit], 
    @OrderPrice [decimal](18, 4), 
    @FilledPrice [decimal](18, 4), 
    @OrderQuantity [decimal](18, 4), 
    @FilledQuantity [decimal](18, 4), 
    @OrderAmount [decimal](18, 4), 
    @FilledAmount [decimal](18, 4) 
As
Begin
    Declare @newKey        int

    Insert Into [dbo].[StopLossOrders](
            [TradeId], [StopAll], [OrderPrice], [FilledPrice], 
            [OrderQuantity], [FilledQuantity], [OrderAmount], 
            [FilledAmount], [DisplayOrder])
    Values (@TradeId, @StopAll, @OrderPrice, @FilledPrice, 
            @OrderQuantity, @FilledQuantity, @OrderAmount, 
            @FilledAmount, 1000)

    Set @newKey = SCOPE_IDENTITY();
    Exec [log].[StopLossOrdersLog_Insert]
        @StopLossOrderId = @newKey,
        @LogStatus = 1010,
        @LogStatusDescription = N'Yeni Kayıt';

    Select  [StopLossOrders].[Id],
            [StopLossOrders].[TradeId], 
            [StopLossOrders].[StopAll], 
            [StopLossOrders].[ExecuteTime], 
            [StopLossOrders].[OrderPrice], 
            [StopLossOrders].[FilledPrice], 
            [StopLossOrders].[OrderQuantity], 
            [StopLossOrders].[FilledQuantity], 
            [StopLossOrders].[OrderAmount], 
            [StopLossOrders].[FilledAmount], 
            [StopLossOrders].[DisplayOrder] 
    From  [dbo].[StopLossOrders]
    Where [dbo].[StopLossOrders].[Id] = @newKey
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[StopLossOrders_Update]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StopLossOrders_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[StopLossOrders_Update]
Go
CREATE PROCEDURE [dbo].[StopLossOrders_Update]
    @Id [int], 
    @StopAll [bit], 
    @ExecuteTime [DateTime], 
    @OrderPrice [decimal](18, 4), 
    @FilledPrice [decimal](18, 4), 
    @OrderQuantity [decimal](18, 4), 
    @FilledQuantity [decimal](18, 4), 
    @OrderAmount [decimal](18, 4), 
    @FilledAmount [decimal](18, 4) 
As
Begin
    Update [dbo].[StopLossOrders]
       Set [StopAll] = @StopAll, [ExecuteTime] = @ExecuteTime, 
           [OrderPrice] = @OrderPrice, 
           [FilledPrice] = @FilledPrice, 
           [OrderQuantity] = @OrderQuantity, 
           [FilledQuantity] = @FilledQuantity, 
           [OrderAmount] = @OrderAmount, 
           [FilledAmount] = @FilledAmount
     Where [dbo].[StopLossOrders].[Id] = @Id

    Exec [log].[StopLossOrdersLog_Insert]
        @StopLossOrderId = @Id,
        @LogStatus = 1020,
        @LogStatusDescription = N'Genel Güncelleme';

    Select  [StopLossOrders].[Id],
            [StopLossOrders].[TradeId], 
            [StopLossOrders].[StopAll], 
            [StopLossOrders].[ExecuteTime], 
            [StopLossOrders].[OrderPrice], 
            [StopLossOrders].[FilledPrice], 
            [StopLossOrders].[OrderQuantity], 
            [StopLossOrders].[FilledQuantity], 
            [StopLossOrders].[OrderAmount], 
            [StopLossOrders].[FilledAmount], 
            [StopLossOrders].[DisplayOrder] 
    From  [dbo].[StopLossOrders]
    Where [dbo].[StopLossOrders].[Id] = @Id
End
Go
