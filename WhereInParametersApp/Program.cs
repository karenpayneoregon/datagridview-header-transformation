using System.Data;
using System.Diagnostics;
using System.Reflection;
using ConfigurationLibrary.Classes;
using DbPeekQueryLibrary.LanguageExtensions;
using Microsoft.Data.SqlClient;
using SqlServerLibrary.Classes;
using SqlServerLibrary.Extensions;
using WhereInParametersApp.Models;

namespace WhereInParametersApp;

internal partial class Program
{

    static void Main(string[] args)
    {
        string connectionString = 
            """
            Server=(localdb)\MSSQLLocalDB;
            Database=NorthWind2022;
            Trusted_Connection=True
            """;

        List<int> identifiers = new() { 3, 34, 24 };
        string preFix = "id";

        using SqlConnection cn = new(connectionString);
        using SqlCommand cmd = new(null, cn);

        cmd.WhereInParameters(SqlStatements.WhereInForCustomers, preFix, identifiers);
        cn.Open();
        DataTable dt = new();
        dt.Load(cmd.ExecuteReader());
        List<Customer> customers = dt.ToList<Customer>();

        customers.ForEach(Console.WriteLine);

        Console.ReadLine();
    }
}