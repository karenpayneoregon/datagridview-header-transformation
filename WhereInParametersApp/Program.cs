using ConfigurationLibrary.Classes;
using DbPeekQueryLibrary.LanguageExtensions;
using Microsoft.Data.SqlClient;
using SqlServerLibrary.Classes;
using SqlServerLibrary.Extensions;

namespace WhereInParametersApp;

internal partial class Program
{

    static void Main(string[] args)
    {
        List<int> list = new List<int>() { 3, 34, 24 };
        string preFix = "id";

        using var cn = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=NorthWind2022;Trusted_Connection=True");
        using var cmd = new SqlCommand { Connection = cn };

        cmd.CommandText = SqlWhereInParamBuilder
            .BuildInClause(SqlStatements.WhereInForCustomers, preFix, list);

        cmd.AddParamsToCommand(preFix, list);

        Console.WriteLine(cmd.CommandText);
        Console.WriteLine();
        Console.WriteLine(cmd.ActualCommandText());
        Console.WriteLine();

        cn.Open();
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(reader.GetString(1));
        }

        Console.WriteLine("Done");
        
        Console.ReadLine();
    }
}