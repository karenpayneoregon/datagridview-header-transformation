namespace SqlServerLibrary.Classes
{
    public class SqlStatements
    {
        /// <summary>
        /// Provides column names from the description property for each column in a specified table name
        /// </summary>
        public static string DescriptionStatement =>
            """
            SELECT COLUMN_NAME AS ColumnName,
                   ORDINAL_POSITION AS Position,
                   prop.value AS [Description]
            FROM INFORMATION_SCHEMA.TABLES AS tbl
                INNER JOIN INFORMATION_SCHEMA.COLUMNS AS col
                    ON col.TABLE_NAME = tbl.TABLE_NAME
                INNER JOIN sys.columns AS sc
                    ON sc.object_id = OBJECT_ID(tbl.TABLE_SCHEMA + '.' + tbl.TABLE_NAME)
                       AND sc.name = col.COLUMN_NAME
                LEFT JOIN sys.extended_properties prop
                    ON prop.major_id = sc.object_id
                       AND prop.minor_id = sc.column_id
                       AND prop.name = 'MS_Description'
            WHERE tbl.TABLE_NAME = @TableName
            ORDER BY col.ORDINAL_POSITION;
            """;

        /// <summary>
        /// Get default values for each column for tables
        /// </summary>
        public static string GetDefaultValuesInDatabase =>
            """
            SELECT SO.[name] AS "TableName",
                   SC.[name] AS "ColumnName",
                   SM.[text] AS "DefaultValue"
            FROM dbo.sysobjects SO
                INNER JOIN dbo.syscolumns SC
                    ON SO.id = SC.id
                LEFT JOIN dbo.syscomments SM
                    ON SC.cdefault = SM.id
            WHERE SO.xtype = 'U'
                  AND SO.[name] <> 'sysdiagrams'
                  AND SM.[text] IS NOT NULL
            ORDER BY SO.[name],
                     SC.colid;
            """;

        /// <summary>
        /// Get all database names
        /// </summary>
        public static string GetDatabaseNames =>
            """
            SELECT TableName = DB_NAME(s_mf.database_id)
            FROM sys.master_files s_mf
            WHERE s_mf.state = 0 -- ONLINE
                  AND HAS_DBACCESS(DB_NAME(s_mf.database_id)) = 1
                  AND DB_NAME(s_mf.database_id) NOT IN ( 'master', 'tempdb', 'model', 'msdb' )
                  AND DB_NAME(s_mf.database_id)NOT LIKE 'ReportServer%'
            GROUP BY s_mf.database_id
            ORDER BY 1;
            """;

        /// <summary>
        /// Get details for a table which requires a table name in the calling code
        /// </summary>
        public static string TableDetails =>
            """
            SELECT c.[name] 'ColumnName',
                   t.[name] 'DataType',
                   c.[max_length] 'MaxLength',
                   c.[precision] 'Precision',
                   c.scale 'Scale',
                   c.is_nullable 'IsNullable',
                   ISNULL(i.is_primary_key, 0) 'PrimaryKey'
            FROM sys.columns c
                INNER JOIN sys.types t
                    ON c.user_type_id = t.user_type_id
                LEFT OUTER JOIN sys.index_columns ic
                    ON ic.object_id = c.object_id
                       AND ic.column_id = c.column_id
                LEFT OUTER JOIN sys.indexes i
                    ON ic.object_id = i.object_id
                       AND ic.index_id = i.index_id
            WHERE c.object_id = OBJECT_ID(@TableName);
            """;

        /// <summary>
        /// Get all computed columns in a database
        /// </summary>
        public static string ComputedColumnDefinitions =>
            """
            SELECT SCHEMA_NAME(o.schema_id) 'SchemaName',
                   c.name AS 'ColumnName',
                   OBJECT_NAME(c.object_id) AS 'TableName',
                   TYPE_NAME(c.user_type_id) AS 'DataType',
                   c.definition 'Definition'
            FROM sys.computed_columns c
                JOIN sys.objects o
                    ON o.object_id = c.object_id
            ORDER BY SchemaName,
                     TableName,
                     c.column_id;
            """;
    }
}
