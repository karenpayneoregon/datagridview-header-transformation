using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace WhereInParametersApp;
internal partial class Program
{
    private static string _connectionString =
        """
        Server=(localdb)\MSSQLLocalDB;
        Database=NorthWind2022;
        Trusted_Connection=True
        """;
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "Code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
