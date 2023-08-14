using Microsoft.Data.SqlClient;
using SqlServerLibrary.Classes;
using System.Data;
using DbPeekQueryLibrary.LanguageExtensions;
using Serilog;

namespace Experiments.Classes;
internal class DummyCommands
{
    /// <summary>
    /// Demonstrates revealing parameter values for a SQL statement
    /// Outputs to a SeriLog file under Debug\LogFiles with a timestamp folder for today
    /// </summary>
    /// <remarks>
    /// See https://www.nuget.org/packages/DbPeekQueryLibrary/
    /// </remarks>
    public static void ShowCommandParameters()
    {
        string connectionString = Utilities.NorthWindConnectionString;
        using var cn = new SqlConnection(connectionString);
        var cmd = new SqlCommand { Connection = cn, CommandText = SqlStatements.TableExists };
        cmd.Parameters.Add("@TableName", SqlDbType.NVarChar).Value = "Customers";

        // write statement with values
        Log.Information(cmd.ActualCommandText());
    }
}
