using Experiments.Classes;
using Microsoft.Extensions.DependencyInjection;
using SqlServerLibrary.Classes;
using SqlServerLibrary.Models;
using static Experiments.Classes.SpectreConsoleHelpers;
using static Experiments.Classes.Utilities;

namespace Experiments;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        var services = ConfigureServices();
        await using var serviceProvider = services.BuildServiceProvider();

        AnsiConsole.MarkupLine("[cyan]Table descriptions from two different databases[/]");

        ColumnsService columnsService = serviceProvider.GetService<ColumnsService>();

        List<ColumnDescriptions> booksList = columnsService.ForBooks(BooksConnection);
        ColumnHelpers.ForBooks(booksList);

        List<ColumnDescriptions> contactsList = columnsService.Contacts(NorthWindConnectionString);
        ColumnHelpers.ForContacts(contactsList);

        AnsiConsole.MarkupLine("[cyan]Misc helpers[/]");


        List<ComputedColumns> computedColumnsList = columnsService.GetComputedColumnsList(ComputedConnection);
        ColumnHelpers.GetComputedColumns(computedColumnsList);

        List<DateTimeContainer> dateTimeColumns = columnsService.GetDateTimeColumns(Utilities.NorthWindConnectionString);
        ColumnHelpers.GetDateTimeInformation(dateTimeColumns);


        List<TableConstraints>  tableConstraintsList = serviceProvider.GetService<ConstraintsService>()
            .GetAll(NorthWindConnectionString);

        ConstraintHelpers.GetTablesWithDeleteRuleForNorthWindDatabase(tableConstraintsList);

        DummyCommands.ShowCommandParameters();
        AnsiConsole.MarkupLine("[cyan]Query logged with [/][yellow]SeriLog[/]");


        ExitPrompt();
    }
}