using Microsoft.Data.SqlClient;
using SqlServerLibrary.Classes;
using System.Data;
using DbPeekQueryLibrary.LanguageExtensions;
using Serilog;

namespace Experiments.Classes;
internal class DummyCommands
{
    /// <summary>
    /// Demonstrates revealing parameter values for a SQL statement which is useful when allowing a user to enter
    /// values for parameters and there are incorrect or no results. This allows a developer to see what a
    /// user entered.
    /// 
    /// Outputs to a SeriLog file under Debug\LogFiles with a timestamp folder for today
    /// </summary>
    /// <remarks>
    /// See https://www.nuget.org/packages/DbPeekQueryLibrary/
    /// </remarks>
    public static void ShowCommandParameters()
    {
        string connectionString = Utilities.NorthWindConnectionString;
        using var cn = new SqlConnection(connectionString);

        // Simple example
        var cmd = new SqlCommand { Connection = cn, CommandText = SqlStatements.TableExists };
        cmd.Parameters.Add("@TableName", SqlDbType.NVarChar).Value = "Customers";

        // write statement with values
        Log.Information($"{cmd.ActualCommandText()}\n\n---------------------------------\n");

        cmd.Parameters.Clear();

        // complex example
        cmd.Parameters.Add("@CustomerIdentifier", SqlDbType.Int).Value = 4;
        cmd.Parameters.Add("@PhoneType", SqlDbType.Int).Value = 3;
        cmd.Parameters.Add("@ContactType", SqlDbType.Int).Value = 12;

        cmd.CommandText = SqlStatements.GetCustomers;
















        cn.Open();
        var reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            // with current parameter values we land here
        }
        else
        {
            // when no results we land here, log it.
        }

        // for this demo we log no matter if there are rows or not.
        Log.Information(cmd.ActualCommandText());

    }
}
