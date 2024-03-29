﻿using System.Data;
using Microsoft.Data.SqlClient;
using SqlServerLibrary.Extensions;
using SqlServerLibrary.Models;


namespace SqlServerLibrary.Classes;

public class ColumnService
{
    /// <summary>
    /// Get column descriptions from a SQL-Server table
    /// </summary>
    /// <param name="connectionString">database connection string</param>
    /// <param name="tableName">existing table under database in connection string</param>
    public static List<ColumnDescriptions> ColumnDetails(string connectionString, string tableName)
    {

        List<ColumnDescriptions> list = new();

        using var cn = new SqlConnection(connectionString);
        var cmd = new SqlCommand { Connection = cn, CommandText = SqlStatements.DescriptionStatement };
        cmd.Parameters.Add("@TableName", SqlDbType.NVarChar).Value = tableName;

        cn.Open();

        var reader = cmd.ExecuteReader();

        if (!reader.HasRows) return list;

        while (reader.Read())
        {
            var columnName = reader.GetString(0);
            list.Add(new ColumnDescriptions()
            {
                Name = columnName,
                Ordinal = reader.GetInt32(1),
                Description = reader.GetStringSafe("Description") ?? columnName
            });
        }

        return list;

    }

}