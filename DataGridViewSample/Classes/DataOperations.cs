using DataGridViewLibrary.Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridViewSample.Classes
{
    internal class DataOperations
    {
        public static DataTable Books()
        {
            using var cn = new SqlConnection(ConnectionString());
            var cmd = new SqlCommand { Connection = cn, CommandText = "SELECT Id,Title,Price,CategoryId FROM dbo.Books;" };
            cn.Open();
            DataTable table = new DataTable();

            table.Load(cmd.ExecuteReader());

            return table;
        }
    }
}
