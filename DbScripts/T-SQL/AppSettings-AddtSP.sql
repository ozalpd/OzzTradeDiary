SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go

-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[AppSettings_Delete]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppSettings_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AppSettings_Delete]
Go
CREATE PROCEDURE [dbo].[AppSettings_Delete]
    @Id [int]
As
Begin
    /* TODO: Check cascaded data from other tables */
    Delete From [dbo].[AppSettings]
        Where [dbo].[AppSettings].[Id] = @Id
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[AppSettings_Insert]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppSettings_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AppSettings_Insert]
Go
CREATE PROCEDURE [dbo].[AppSettings_Insert]
    @SettingType [int], 
    @SettingName [nVarChar](64), 
    @SettingValue [nVarChar](2048), 
    @Description [nVarChar](255) 
As
Begin
    Declare @newKey        int

    Insert Into [dbo].[AppSettings](
            [SettingType], [SettingName], [SettingValue], 
            [Description])
    Values (@SettingType, @SettingName, @SettingValue, 
            @Description)

    Set @newKey = SCOPE_IDENTITY();

    Select  [AppSettings].[Id],
            [AppSettings].[SettingType], 
            [AppSettings].[SettingName], 
            [AppSettings].[SettingValue], 
            [AppSettings].[Description] 
    From  [dbo].[AppSettings]
    Where [dbo].[AppSettings].[Id] = @newKey
End
Go
-- --------------------------------------------------------------------
-- Stored Procedure: [dbo].[AppSettings_Update]
-- --------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppSettings_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AppSettings_Update]
Go
CREATE PROCEDURE [dbo].[AppSettings_Update]
    @Id [int], 
    @SettingType [int], 
    @SettingName [nVarChar](64), 
    @SettingValue [nVarChar](2048), 
    @Description [nVarChar](255) 
As
Begin
    Update [dbo].[AppSettings]
       Set [SettingType] = @SettingType, 
           [SettingName] = @SettingName, 
           [SettingValue] = @SettingValue, 
           [Description] = @Description
     Where [dbo].[AppSettings].[Id] = @Id

    Select  [AppSettings].[Id],
            [AppSettings].[SettingType], 
            [AppSettings].[SettingName], 
            [AppSettings].[SettingValue], 
            [AppSettings].[Description] 
    From  [dbo].[AppSettings]
    Where [dbo].[AppSettings].[Id] = @Id
End
Go
