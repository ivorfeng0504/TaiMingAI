CREATE TABLE [dbo].[Administrator]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LoginName] NVARCHAR(10) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [Role] VARCHAR(50) NOT NULL, 
    [RoleName] NVARCHAR(100) NULL, 
    [Mobile] VARCHAR(11) NULL, 
    [Email] VARCHAR(50) NULL, 
    [NickName] NVARCHAR(50) NULL, 
    [State] INT NOT NULL DEFAULT 0, 
    [CreateTime] DATETIME NOT NULL DEFAULT getdate(), 
    [UpdateTime] DATETIME NOT NULL DEFAULT getdate()
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户ID',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Administrator',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'手机号码（11位）',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Administrator',
    @level2type = N'COLUMN',
    @level2name = N'Mobile'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'密码（MD5 32位加密）',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Administrator',
    @level2type = N'COLUMN',
    @level2name = N'Password'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'登录名',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Administrator',
    @level2type = N'COLUMN',
    @level2name = 'LoginName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'邮箱',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Administrator',
    @level2type = N'COLUMN',
    @level2name = N'Email'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'昵称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Administrator',
    @level2type = N'COLUMN',
    @level2name = N'NickName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'角色Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Administrator',
    @level2type = N'COLUMN',
    @level2name = N'Role'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'角色名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Administrator',
    @level2type = N'COLUMN',
    @level2name = N'RoleName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'-1:注销；0:未审核；1:审核通过；',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Administrator',
    @level2type = N'COLUMN',
    @level2name = N'State'