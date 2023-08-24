using ConfigurationLibrary.Classes;
using DbPeekQueryLibrary.LanguageExtensions;
using Microsoft.Data.SqlClient;
using SqlServerLibrary.Extensions;

namespace WhereInParametersApp;

internal partial class Program
{

    static void Main(string[] args)
    {
        List<int> list = new List<int>() { 3, 34, 24 };
        using var cn = new SqlConnection(ConfigurationHelper.ConnectionString());
        using var cmd = new SqlCommand { Connection = cn };

        cmd.CommandText = SqlWhereInParamBuilder
            .BuildInClause("SELECT CompanyName FROM dbo.Customer WHERE Identifier IN ({0})", "Identifier",
                list);

        cmd.AddParamsToCommand("Identifier", list);

        Console.WriteLine(cmd.CommandText);
        Console.WriteLine();
        Console.WriteLine(cmd.ActualCommandText());
        
        Console.ReadLine();
    }
}