SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go

-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[TradingAccounts_Delete]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TradingAccounts_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TradingAccounts_Delete]
Go
CREATE PROCEDURE [dbo].[TradingAccounts_Delete]
    @Id [int]
As
Begin
    /* TODO: Check cascaded data from other tables */
    Delete From [dbo].[TradingAccounts]
        Where [dbo].[TradingAccounts].[Id] = @Id
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[TradingAccounts_Insert]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TradingAccounts_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TradingAccounts_Insert]
Go
CREATE PROCEDURE [dbo].[TradingAccounts_Insert]
    @Title [nVarChar](128), 
    @AccountCode [nVarChar](20), 
    @ExchangeId [int], 
    @Notes [nVarChar](max) 
As
Begin
    Declare @newKey        int

    Insert Into [dbo].[TradingAccounts](
            [Title], [AccountCode], [ExchangeId], [Notes], [DisplayOrder], 
            [IsActive])
    Values (@Title, @AccountCode, @ExchangeId, @Notes, 1000, 
            1)

    Set @newKey = SCOPE_IDENTITY();

    Select  [TradingAccounts].[Id],
            [TradingAccounts].[Title], 
            [TradingAccounts].[AccountCode], 
            [TradingAccounts].[ExchangeId], 
            [TradingAccounts].[Notes], 
            [TradingAccounts].[DisplayOrder], 
            [TradingAccounts].[IsActive] 
    From  [dbo].[TradingAccounts]
    Where [dbo].[TradingAccounts].[Id] = @newKey
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[TradingAccounts_Update]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TradingAccounts_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TradingAccounts_Update]
Go
CREATE PROCEDURE [dbo].[TradingAccounts_Update]
    @Id [int], 
    @Title [nVarChar](128), 
    @AccountCode [nVarChar](20), 
    @Notes [nVarChar](max) 
As
Begin
    Update [dbo].[TradingAccounts]
       Set [Title] = @Title, [AccountCode] = @AccountCode, 
           [Notes] = @Notes
     Where [dbo].[TradingAccounts].[Id] = @Id

    Select  [TradingAccounts].[Id],
            [TradingAccounts].[Title], 
            [TradingAccounts].[AccountCode], 
            [TradingAccounts].[ExchangeId], 
            [TradingAccounts].[Notes], 
            [TradingAccounts].[DisplayOrder], 
            [TradingAccounts].[IsActive] 
    From  [dbo].[TradingAccounts]
    Where [dbo].[TradingAccounts].[Id] = @Id
End
Go
