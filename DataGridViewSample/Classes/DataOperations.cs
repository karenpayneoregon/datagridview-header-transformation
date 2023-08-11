using Microsoft.Data.SqlClient;
using System.Data;

namespace DataGridViewSample.Classes;

internal class DataOperations
{
    public static DataTable Books()
    {
        using var cn = new SqlConnection(ConnectionString());
        var cmd = new SqlCommand { Connection = cn, CommandText = "SELECT Id,Title,Price,CategoryId FROM dbo.Books;" };
        cn.Open();
        DataTable table = new();

        table.Load(cmd.ExecuteReader());

        return table;
    }
}