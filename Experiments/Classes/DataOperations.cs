using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlServerLibrary.Classes;
using SqlServerLibrary.Models;

namespace Experiments.Classes;

public class DataOperations
{
    private readonly ConnectionStrings _options;
    private readonly ILogger<DataOperations> _logger;
    public DataOperations(IOptions<ConnectionStrings> options, ILogger<DataOperations> logger)
    {
        _options = options?.Value;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Display connection string. In a real application we do something
    /// like read data from a database
    /// </summary>
    public async Task ReadConnectionString()
    {
        Helpers.PrintSampleName();

        _logger.LogInformation("Getting connection string\n");

        AnsiConsole.MarkupLine($"[yellow]Connection string[/] [cyan]{_options.NorthWindConnection}[/]");
        AnsiConsole.MarkupLine($"[yellow]Connection string[/] [cyan]{_options.BooksConnection}[/]");

        await Task.CompletedTask;
    }
    //var columns = ColumnOperations.ColumnDetails(ConnectionString(), "Books");

    public void GetColumnDescriptionsForBooks()
    {
        Helpers.PrintSampleName();

        List<DataColumn> columns = ColumnOperations.ColumnDetails(_options.BooksConnection, "Books");
        var table = CreateViewTable();
        foreach (var column in columns)
        {
            table.AddRow(column.Ordinal.ToString(), column.Name,column.Description);
        }
        AnsiConsole.Write(table);
    }

    public void GetColumnDescriptionsForContacts()
    {
        Helpers.PrintSampleName();

        List<DataColumn> columns = ColumnOperations.ColumnDetails(_options.NorthWindConnection, "Contacts");
        var table = CreateViewTable();
        foreach (var column in columns)
        {
            table.AddRow(column.Ordinal.ToString(), column.Name, column.Description);
        }
        AnsiConsole.Write(table);
    }

    private static Table CreateViewTable() =>
        new Table()
            .Border(TableBorder.Square)
            .BorderColor(Color.Grey100)
            .Alignment(Justify.Center)
            .Title("[white on blue][B]Books column descriptions[/][/]")
            .AddColumn(new TableColumn("[u]Ordinal[/]"))
            .AddColumn(new TableColumn("[u]Name[/]"))
            .AddColumn(new TableColumn("[u]Description[/]"));

}
