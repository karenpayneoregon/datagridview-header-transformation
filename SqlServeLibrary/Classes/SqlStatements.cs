namespace SqlServerLibrary.Classes
{
    /// <summary>
    /// A container for all SQL statements
    /// </summary>
    /// <remarks>
    /// All statements are created in SSMS and pasted here.
    /// As a rule of thumb its better to write statements in SSMS, validate they work
    /// as expected than added them as done here.
    /// </remarks>
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

        /// <summary>
        /// Determine if table exists in a data
        /// </summary>
        public static string TableExists =>
            """
            SELECT CASE
                WHEN EXISTS
                     (
                         (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName)
                     ) THEN
                    1
                ELSE
                    0
            END;
            """;

        /// <summary>
        /// Determine if table exists in a data
        /// </summary>
        public static string TableConstraintsForDatabase =>
            """
            SELECT
                PrimaryKeyTable = QUOTENAME(PK.CONSTRAINT_SCHEMA) + '.' + QUOTENAME(PK.TABLE_NAME),
                ConstraintName = C.CONSTRAINT_NAME,
                PrimaryKeyColumn = CCU.COLUMN_NAME,
                ForeignKeyTable = QUOTENAME(FK.CONSTRAINT_SCHEMA) + '.' + QUOTENAME(FK.TABLE_NAME),
                ForeignKeyColumn = CU.COLUMN_NAME,
                UpdateRule = C.UPDATE_RULE,
                DeleteRule = C.DELETE_RULE
            FROM
                INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C 
                INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON 
                    C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME AND
                    C.CONSTRAINT_CATALOG = FK.CONSTRAINT_CATALOG AND
                    C.CONSTRAINT_SCHEMA = FK.CONSTRAINT_SCHEMA
                INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON 
                    C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME AND
                    C.UNIQUE_CONSTRAINT_CATALOG = PK.CONSTRAINT_CATALOG AND
                    C.UNIQUE_CONSTRAINT_SCHEMA = PK.CONSTRAINT_SCHEMA
                INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON 
                    C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME AND
                    C.CONSTRAINT_CATALOG = CU.CONSTRAINT_CATALOG AND
                    C.CONSTRAINT_SCHEMA = CU.CONSTRAINT_SCHEMA
                INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CCU ON 
                    PK.CONSTRAINT_NAME = CCU.CONSTRAINT_NAME AND
                    PK.CONSTRAINT_CATALOG = CCU.CONSTRAINT_CATALOG AND
                    PK.CONSTRAINT_SCHEMA = CCU.CONSTRAINT_SCHEMA
            WHERE
                FK.CONSTRAINT_TYPE = 'FOREIGN KEY'
            ORDER BY
                PK.TABLE_NAME, 
                FK.TABLE_NAME
            """;
    }
}
