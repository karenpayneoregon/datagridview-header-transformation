using System.Data;
using DataGridViewLibrary.Classes;
using Microsoft.Data.SqlClient;
using DataColumn = DataGridViewLibrary.Models.DataColumn;

namespace DataGridViewLibrary
{
    public class ColumnOperations
    {
        /// <summary>
        /// Get column descriptions
        /// </summary>
        /// <param name="pConnectionString">database connection string</param>
        /// <param name="pTableName">existing table under database in connection string</param>
        /// <remarks>
        /// There is no exception handling, for those using this consider adding some form
        /// of assertion via try/catch
        /// </remarks>
        public static List<DataColumn> ColumnDetails(string pConnectionString, string pTableName)
        {
            List<DataColumn> list = new List<DataColumn>();

            using var cn = new SqlConnection(pConnectionString);
            var cmd = new SqlCommand { Connection = cn, CommandText = SqlStatements.DescriptionStatement };
            cmd.Parameters.Add("@TableName", SqlDbType.NVarChar).Value = pTableName;

            cn.Open();

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows) return list;
            while (reader.Read())
            {
                list.Add(new DataColumn()
                {
                    Name = reader.GetString(0),
                    Ordinal = reader.GetInt32(1),
                    Description = reader.GetStringSafe("Description")
                });

            }
            return list;

        }
    }
}