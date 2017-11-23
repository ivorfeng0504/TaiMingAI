CREATE TABLE [dbo].[TmingUserInfo]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(10) NULL, 
    [Powsword] NCHAR(50) NULL, 
    [Mobile] INT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户ID',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TmingUserInfo',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'手机号码（11位）',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TmingUserInfo',
    @level2type = N'COLUMN',
    @level2name = N'Mobile'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'密码（MD5 32位加密）',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TmingUserInfo',
    @level2type = N'COLUMN',
    @level2name = N'Powsword'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户名',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TmingUserInfo',
    @level2type = N'COLUMN',
    @level2name = N'Name'