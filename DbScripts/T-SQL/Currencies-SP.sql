SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go

-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[Currencies_Delete]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Currencies_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Currencies_Delete]
Go
CREATE PROCEDURE [dbo].[Currencies_Delete]
    @Id [int]
As
Begin
    /* TODO: Check cascaded data from other tables */
    Delete From [dbo].[Currencies]
        Where [dbo].[Currencies].[Id] = @Id
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[Currencies_Insert]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Currencies_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Currencies_Insert]
Go
CREATE PROCEDURE [dbo].[Currencies_Insert]
    @CurrencyTicker [nVarChar](8), 
    @Description [nVarChar](255) 
As
Begin
    Declare @newKey        int

    Insert Into [dbo].[Currencies](
            [CurrencyTicker], [Description], [DisplayOrder], [IsActive])
    Values (@CurrencyTicker, @Description, 1000, 1)

    Set @newKey = SCOPE_IDENTITY();

    Select  [Currencies].[Id],
            [Currencies].[CurrencyTicker], 
            [Currencies].[Description], 
            [Currencies].[DisplayOrder], 
            [Currencies].[IsActive] 
    From  [dbo].[Currencies]
    Where [dbo].[Currencies].[Id] = @newKey
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[Currencies_Update]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Currencies_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Currencies_Update]
Go
CREATE PROCEDURE [dbo].[Currencies_Update]
    @Id [int], 
    @Description [nVarChar](255) 
As
Begin
    Update [dbo].[Currencies]
       Set [Description] = @Description
     Where [dbo].[Currencies].[Id] = @Id

    Select  [Currencies].[Id],
            [Currencies].[CurrencyTicker], 
            [Currencies].[Description], 
            [Currencies].[DisplayOrder], 
            [Currencies].[IsActive] 
    From  [dbo].[Currencies]
    Where [dbo].[Currencies].[Id] = @Id
End
Go
