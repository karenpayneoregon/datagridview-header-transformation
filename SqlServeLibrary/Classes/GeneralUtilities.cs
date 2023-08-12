using System.Data;
using Microsoft.Data.SqlClient;

namespace SqlServerLibrary.Classes;

public class GeneralUtilities
{
    public static bool TablesArePopulated(string connectionString)
    {
        using var cn = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(
            """
                   SELECT T.name TableName, i.Rows NumberOfRows 
                   FROM sys.tables T JOIN sys.sysindexes I ON T.OBJECT_ID = I.ID 
                   WHERE indid IN (0,1) 
                   ORDER BY i.Rows DESC,T.name
                   """, cn);

        DataTable table = new();
        cn.Open();

        table.Load(cmd.ExecuteReader());
        return table.AsEnumerable().All(row => row.Field<int>("NumberOfRows") > 0);

    }

    /// <summary>
    /// Determine the database has records
    /// </summary>
    /// <param name="databaseName">name of database</param>
    /// <returns>true if all tables have records</returns>
    public static bool ExpressDatabaseExists(string databaseName)
    {
        using var cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=master;integrated security=True;Encrypt=False");
        using var cmd = new SqlCommand($"SELECT DB_ID('{databaseName}'); ", cn);

        cn.Open();
        return cmd.ExecuteScalar() != DBNull.Value;

    }
    /// <summary>
    /// Determine the database has records
    /// </summary>
    /// <param name="databaseName">name of database</param>
    /// <returns>true if all tables have records</returns>
    public static bool LocalDbDatabaseExists(string databaseName)
    {
        using var cn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;integrated security=True;Encrypt=False");
        using var cmd = new SqlCommand($"SELECT DB_ID('{databaseName}'); ", cn);

        cn.Open();
        return cmd.ExecuteScalar() != DBNull.Value;

    }

    /// <summary>
    /// Get InitialCatalog from a valid connection string
    /// </summary>
    /// <param name="connectionString">Valid constructed connection string</param>
    /// <returns>InitialCatalog from <param name="connectionString">connectionString</param></returns>
    public static string InitialCatalogFromConnectionString(string connectionString) 
        => new SqlConnectionStringBuilder(connectionString).InitialCatalog;

    /// <summary>
    /// Get DataSource from a valid connection string
    /// </summary>
    /// <param name="connectionString">Valid constructed connection string</param>
    /// <returns>DataSource from <param name="connectionString">connectionString</param></returns>
    public static string DataSourceFromConnectionString(string connectionString)
        => new SqlConnectionStringBuilder(connectionString).DataSource;
}