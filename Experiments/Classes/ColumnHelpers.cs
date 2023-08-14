using SqlServerLibrary.Models;


namespace Experiments.Classes;

/// <summary>
/// Example for obtaining column descriptions from SQL-Server tables which are defined in design pane of a table in SSMS (SQL-Server Management Studio)
/// ForBooks and ForContacts could also be generic, one method
/// </summary>
public class ColumnHelpers
{

    public static void ForBooks(List<ColumnDescriptions> results)
    {
        ConsoleHelpers.PrintSampleName();
        DisplayColumns(results);
    }

    public static void ForContacts(List<ColumnDescriptions> results)
    {
        ConsoleHelpers.PrintSampleName();
        DisplayColumns(results);
    }

    private static void DisplayColumns(List<ColumnDescriptions> columns)
    {
        var table = CreateTable();
        foreach (var column in columns)
        {
            table.AddRow(column.Ordinal.ToString(), column.Name, column.Description);
        }

        AnsiConsole.Write(table);
    }

    public static void GetComputedColumns(List<ComputedColumns> results)
    {
        ConsoleHelpers.PrintSampleName();

        if (results != null)
        {
            foreach (var column in results)
            {
                Console.WriteLine($"{column.SchemaName}.{column.TableName,-20}{column.ColumnName,-20}{column.DataType}");
                Console.WriteLine($"\t{column.Definition}");
            }
        }
        else
        {
            Console.WriteLine("No computed columns");
        }
    }

    public static void GetDateTimeInformation(List<DateTimeContainer> results)
    {
        Console.WriteLine();

        ConsoleHelpers.PrintSampleName();


        foreach (var container in results)
        {
            Console.WriteLine(container);
        }

        Console.WriteLine();

    }

    private static Table CreateTable() =>
        new Table()
            .Border(TableBorder.Square)
            .BorderColor(Color.Grey100)
            .Alignment(Justify.Center)
            .Title("[white][B]Books column descriptions[/][/]")
            .AddColumn(new TableColumn("[cyan]Ordinal[/]"))
            .AddColumn(new TableColumn("[cyan]Name[/]"))
            .AddColumn(new TableColumn("[cyan]Description[/]"));

}
