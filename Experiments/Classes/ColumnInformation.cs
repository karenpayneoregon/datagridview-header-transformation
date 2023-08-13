using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlServerLibrary.Classes;


namespace Experiments.Classes;

/// <summary>
/// Example for obtaining column descriptions from SQL-Server tables which are defined in design pane of a table in SSMS (SQL-Server Management Studio)
/// </summary>
public class ColumnInformation
{
    private readonly ConnectionStrings _options;
    private readonly ILogger<ColumnInformation> _logger;
    public ColumnInformation(IOptions<ConnectionStrings> options, ILogger<ColumnInformation> logger)
    {
        _options = options?.Value;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
        var test = GeneralUtilities.DataSourceFromConnectionString(_options!.BooksConnection);
    }


    public void ForBooks()
    {
        Helpers.PrintSampleName();

        List<SqlServerLibrary.Models.ColumnDescriptions> columns = ColumnOperations.ColumnDetails(_options.BooksConnection, "Books");
        DisplayColumns(columns);
    }

    public void Contacts()
    {
        Helpers.PrintSampleName();

        List<SqlServerLibrary.Models.ColumnDescriptions> columns = ColumnOperations.ColumnDetails(_options.NorthWindConnection, "Contacts");
        DisplayColumns(columns);
    }

    private static void DisplayColumns(List<SqlServerLibrary.Models.ColumnDescriptions> columns)
    {
        var table = CreateTable();
        foreach (var column in columns)
        {
            table.AddRow(column.Ordinal.ToString(), column.Name, column.Description);
        }

        AnsiConsole.Write(table);
    }

    public void GetComputedColumns()
    {
        Helpers.PrintSampleName();
        var results =ColumnOperations.GetComputedColumnsList(_options.ComputedConnection);
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
