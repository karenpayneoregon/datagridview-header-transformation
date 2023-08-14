using Microsoft.Data.SqlClient;
using SqlServerLibrary.Interfaces;
using SqlServerLibrary.Models;

namespace SqlServerLibrary.Classes;

public class ConstraintsService : IConstraintsService
{
    /// <summary>
    /// Get table constraints for a database
    /// </summary>
    /// <returns></returns>
    public List<TableConstraints> GetAll(string connectionString)
    {

        List<TableConstraints> list = new();

        using var cn = new SqlConnection(connectionString);
        var cmd = new SqlCommand { Connection = cn, CommandText = SqlStatements.TableConstraintsForDatabase };

        cn.Open();

        var reader = cmd.ExecuteReader();

        if (!reader.HasRows) return list;
        while (reader.Read())
        {
            list.Add(new TableConstraints()
            {
                PrimaryKeyTable = reader.GetString(0),
                ConstraintName = reader.GetString(1),
                PrimaryKeyColumn = reader.GetString(2),
                ForeignKeyTable = reader.GetString(3),
                ForeignKeyColumn = reader.GetString(4),
                UpdateRule = reader.GetString(5),
                DeleteRule = reader.GetString(6)
            });
        }

        return list;
    }
}
