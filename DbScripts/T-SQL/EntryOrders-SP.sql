SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go

-- --------------------------------------------------------------------
-- Stored Procedure: [log].[EntryOrdersLog_Insert]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[log].[EntryOrdersLog_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [log].[EntryOrdersLog_Insert]
Go
CREATE PROCEDURE [log].[EntryOrdersLog_Insert]
    @EntryOrderId [int],
    @LogStatus [int],
    @LogStatusDescription [nVarChar](100)
As
Begin
    Insert Into [log].[EntryOrdersLog](
            [EntryOrderId],
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
    From  [dbo].[EntryOrders]
    Where [dbo].[EntryOrders].[Id] = @EntryOrderId;
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[EntryOrders_Delete]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntryOrders_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntryOrders_Delete]
Go
CREATE PROCEDURE [dbo].[EntryOrders_Delete]
    @Id [int]
As
Begin
    /* Silinmeden önceki son hali loglanıyor... */
    Exec [log].[EntryOrdersLog_Insert]
        @EntryOrderId = @Id,
        @LogStatus = 4010,
        @LogStatusDescription = N'Siliniyor...';

    /* TODO: Check cascaded data from other tables */
    Delete From [dbo].[EntryOrders]
        Where [dbo].[EntryOrders].[Id] = @Id
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[EntryOrders_Insert]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntryOrders_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntryOrders_Insert]
Go
CREATE PROCEDURE [dbo].[EntryOrders_Insert]
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

    Insert Into [dbo].[EntryOrders](
            [TradeId], [OrderPrice], [FilledPrice], [OrderQuantity], 
            [FilledQuantity], [OrderAmount], [FilledAmount], 
            [DisplayOrder])
    Values (@TradeId, @OrderPrice, @FilledPrice, @OrderQuantity, 
            @FilledQuantity, @OrderAmount, @FilledAmount, 
            1000)

    Set @newKey = SCOPE_IDENTITY();
    Exec [log].[EntryOrdersLog_Insert]
        @EntryOrderId = @newKey,
        @LogStatus = 1010,
        @LogStatusDescription = N'Yeni Kayıt';

    Select  [EntryOrders].[Id],
            [EntryOrders].[TradeId], 
            [EntryOrders].[ExecuteTime], 
            [EntryOrders].[OrderPrice], 
            [EntryOrders].[FilledPrice], 
            [EntryOrders].[OrderQuantity], 
            [EntryOrders].[FilledQuantity], 
            [EntryOrders].[OrderAmount], 
            [EntryOrders].[FilledAmount], 
            [EntryOrders].[DisplayOrder] 
    From  [dbo].[EntryOrders]
    Where [dbo].[EntryOrders].[Id] = @newKey
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[EntryOrders_Update]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntryOrders_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntryOrders_Update]
Go
CREATE PROCEDURE [dbo].[EntryOrders_Update]
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
    Update [dbo].[EntryOrders]
       Set [ExecuteTime] = @ExecuteTime, 
           [OrderPrice] = @OrderPrice, 
           [FilledPrice] = @FilledPrice, 
           [OrderQuantity] = @OrderQuantity, 
           [FilledQuantity] = @FilledQuantity, 
           [OrderAmount] = @OrderAmount, 
           [FilledAmount] = @FilledAmount
     Where [dbo].[EntryOrders].[Id] = @Id

    Exec [log].[EntryOrdersLog_Insert]
        @EntryOrderId = @Id,
        @LogStatus = 1020,
        @LogStatusDescription = N'Genel Güncelleme';

    Select  [EntryOrders].[Id],
            [EntryOrders].[TradeId], 
            [EntryOrders].[ExecuteTime], 
            [EntryOrders].[OrderPrice], 
            [EntryOrders].[FilledPrice], 
            [EntryOrders].[OrderQuantity], 
            [EntryOrders].[FilledQuantity], 
            [EntryOrders].[OrderAmount], 
            [EntryOrders].[FilledAmount], 
            [EntryOrders].[DisplayOrder] 
    From  [dbo].[EntryOrders]
    Where [dbo].[EntryOrders].[Id] = @Id
End
Go
