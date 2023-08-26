using System.Data;
using EntityFrameworkLibrary;
using Microsoft.Data.SqlClient;
using SqlServerLibrary.Classes;
using SqlServerLibrary.Extensions;
using WhereInParametersApp.Data;
using WhereInParametersApp.Models;


namespace WhereInParametersApp;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        await EntitySample();
        Console.WriteLine();
        await DataProviderSample();
        Demo();
        Console.ReadLine();
    }
    private static async Task EntitySample()
    {
        List<int> identifiers = new() { 3, 34, 24 };
        await using var context = new Context();

        var customers = await context.WhereInAsync<Customers>(identifiers.ToObjectArray());
        var customers1 = context
            .Customers
            .Where(c => identifiers.Contains(c.CustomerIdentifier))
            .ToList();

        customers.ToList().ForEach(Console.WriteLine);
    }
    private static async Task DataProviderSample()
    {
        List<int> identifiers = new() { 3, 34, 24 };
        string preFix = "id";

        await using SqlConnection cn = new(_connectionString);
        await using SqlCommand cmd = new(null, cn);

        cmd.WhereInConfiguration(SqlStatements.WhereInCustomers, preFix, identifiers);
        await cn.OpenAsync();
        DataTable dt = new();
        dt.Load(await cmd.ExecuteReaderAsync());
        dt.ToList<Customer>().ForEach(Console.WriteLine);
    }
    public void ExampleMethod(int required, string optionalstr)
    {

    }

    private static void Demo()
    {
        Person person = new("Karen","Payne","PayneKaren");
        Console.WriteLine(person);
    }
}