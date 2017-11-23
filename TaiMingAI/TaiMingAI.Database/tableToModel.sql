declare @TableName varchar(20) = '表名'
declare @Result varchar(max) = 'public class ' + @TableName + '
{'

select @Result = @Result + Summary+'
    public ' + ColumnType + NullableSign + ' ' + ColumnName + ' { get; set; }
'
from
(
    select 
        replace(col.name, ' ', '_') ColumnName,
        col.colid ColumnId,
        case typ.name 
            when 'bigint' then 'long'
            when 'binary' then 'byte[]'
            when 'bit' then 'bool'
            when 'char' then 'string'
            when 'date' then 'DateTime'
            when 'datetime' then 'DateTime'
            when 'datetime2' then 'DateTime'
            when 'datetimeoffset' then 'DateTimeOffset'
            when 'decimal' then 'decimal'
            when 'float' then 'float'
            when 'image' then 'byte[]'
            when 'int' then 'int'
            when 'money' then 'decimal'
            when 'nchar' then 'string'
            when 'ntext' then 'string'
            when 'numeric' then 'decimal'
            when 'nvarchar' then 'string'
            when 'real' then 'double'
            when 'smalldatetime' then 'DateTime'
            when 'smallint' then 'short'
            when 'smallmoney' then 'decimal'
            when 'text' then 'string'
            when 'time' then 'TimeSpan'
            when 'timestamp' then 'DateTime'
            when 'tinyint' then 'byte'
            when 'uniqueidentifier' then 'Guid'
            when 'varbinary' then 'byte[]'
            when 'varchar' then 'string'
            else 'UNKNOWN_' + typ.name
        end ColumnType,
        case 
            when col.isnullable = 1 and typ.name in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier') 
            then '?' 
            else '' 
        end NullableSign,
		case
			when  isnull(exc.[value],'')!=''
			then '
	/// <summary>
	/// '+convert(varchar(50), isnull(exc.[value],''))+'
	/// </summary>'
			else ''
		end Summary
    from syscolumns col
        join systypes typ on
            col.xusertype = typ.xusertype
	    inner join sysobjects obj on col.id=obj.id and obj.xtype='U' and obj.name<>'dtproperties'
        left  join sys.extended_properties exc on col.id=exc.major_id and exc.minor_id=col.colid
    where  obj.name = @TableName
) t
order by ColumnId

set @Result = @Result  + '
}'

print @Result