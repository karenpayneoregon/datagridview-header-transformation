using Microsoft.Data.SqlClient;
using SqlServerLibrary.Interfaces;
using SqlServerLibrary.Models;

namespace SqlServerLibrary.Classes;

public class ColumnsService : IColumnsService
{
    public List<ColumnDescriptions> ForBooks(string connectionString) 
        => ColumnService.ColumnDetails(connectionString, "Books");

    public List<ColumnDescriptions> Contacts(string connectionString) 
        => ColumnService.ColumnDetails(connectionString, "Contacts");

    /// <summary>
    /// Get all table that have computed columns
    /// </summary>
    /// <param name="connectionString">database connection string</param>
    /// <returns>A list of computed columns or an empty list</returns>
    public List<ComputedColumns> GetComputedColumnsList(string connectionString)
    {
        List<ComputedColumns> list = new();
        using var cn = new SqlConnection(connectionString);
        var cmd = new SqlCommand { Connection = cn, CommandText = SqlStatements.ComputedColumnDefinitions };

        cn.Open();

        var reader = cmd.ExecuteReader();

        if (!reader.HasRows) return null;

        while (reader.Read())
        {
            list.Add(new ComputedColumns()
            {
                SchemaName = reader.GetString(0),
                ColumnName = reader.GetString(1),
                TableName = reader.GetString(2),
                DataType = reader.GetString(3),
                Definition = reader.GetString(4)
            });
        }

        return list;
    }

    public List<DateTimeContainer> GetDateTimeColumns(string connectionString)
    {
        List<DateTimeContainer> list = new();

        using var cn = new SqlConnection(connectionString);
        var cmd = new SqlCommand { Connection = cn, CommandText = SqlStatements.GetAllDateTimeColumnsInDatabase };

        cn.Open();

        var reader = cmd.ExecuteReader();

        if (!reader.HasRows) return list;

        while (reader.Read())
        {
            list.Add(new DateTimeContainer()
            {
                TableName = reader.GetString(0),
                ColumnId = reader.GetInt32(1),
                ColumnName = reader.GetString(2),
                DataType = reader.GetString(3),
                Scale = reader.GetByte(4)
            });
        }

        return list;
    }
}
