SET ANSI_NULLS ON
Go
SET QUOTED_IDENTIFIER ON
Go
CREATE TABLE [dbo].[AppSettings](
    [Id] [int] Identity(1,1) Not Null,
    [SettingType] [int] Not Null,
    [SettingName] [nVarChar](64) Not Null,
    [SettingValue] [nVarChar](2048) Not Null, /* This entities must be read from DB rapidly, so field of this property limited to 2048 characters */
    [Description] [nVarChar](255) Null, /* This entities must be read from DB rapidly, so field of this property limited to 255 characters */
  CONSTRAINT [PK_AppSettings] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
