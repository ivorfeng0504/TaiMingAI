CREATE TABLE [dbo].[NavBar]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ParentId] INT NOT NULL DEFAULT 0, 
    [title] NVARCHAR(10) NULL, 
    [icon] VARCHAR(30) NULL, 
    [href] NVARCHAR(200) NULL, 
    [spread] BIT NULL DEFAULT 0, 
    [target] BIT NULL DEFAULT 0, 
    [IsShow] BIT NULL DEFAULT 1, 
    [Sort] INT NULL DEFAULT 0, 
    [CreateTime] DATETIME NOT NULL DEFAULT Getdate(), 
    [UpdateTime] DATETIME NOT NULL DEFAULT Getdate()
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'父级ID',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'NavBar',
    @level2type = N'COLUMN',
    @level2name = N'ParentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否展开；0：否；1：是；',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'NavBar',
    @level2type = N'COLUMN',
    @level2name = N'spread'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'链接',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'NavBar',
    @level2type = N'COLUMN',
    @level2name = N'href'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'图标',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'NavBar',
    @level2type = N'COLUMN',
    @level2name = N'icon'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'标题',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'NavBar',
    @level2type = N'COLUMN',
    @level2name = N'title'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否显示;0：否；1：是；',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'NavBar',
    @level2type = N'COLUMN',
    @level2name = N'IsShow'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排序值',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'NavBar',
    @level2type = N'COLUMN',
    @level2name = N'Sort'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否新窗口打开；0：否；1：是；',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'NavBar',
    @level2type = N'COLUMN',
    @level2name = N'target'