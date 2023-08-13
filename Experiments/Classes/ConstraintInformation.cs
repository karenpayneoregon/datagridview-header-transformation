using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlServerLibrary.Models;
using SqlServerLibrary.Classes;
using static SqlServerLibrary.Classes.GeneralUtilities;

namespace Experiments.Classes;
public class ConstraintInformation
{
    private readonly ConnectionStrings _options;
    private readonly ILogger<ConstraintInformation> _logger;

    public ConstraintInformation(IOptions<ConnectionStrings> options, ILogger<ConstraintInformation> logger)
    {
        _options = options?.Value;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Delete tables with cascading delete rule
    /// </summary>
    public void GetTablesWithDeleteRuleForNorthWindDatabase()
    {
        Helpers.PrintSampleName();
        List<TableConstraints> list = ConstraintsOperations.GetAll(_options.NorthWindConnection);
        var deleteRule = list.Where(x => x.DeleteRule == "CASCADE").ToList();
        if (deleteRule.Count > 0)
        {
            var table = CreateTable();
            foreach (var item in deleteRule)
            {
                table.AddRow(
                    item.PrimaryKeyTable.RemoveDoubleQuotes(), 
                    item.ForeignKeyTable.RemoveDoubleQuotes(), 
                    item.ConstraintName);
                
            }

            AnsiConsole.Write(table);
        }
        else
        {
            AnsiConsole.MarkupLine("[yellow]None found[/]");
        }


    }

    private Table CreateTable() =>
        new Table()
            .Border(TableBorder.Square)
            .BorderColor(Color.Grey100)
            .Alignment(Justify.Center)
            .Title($"[white][B]Tables with delete constraints for {InitialCatalogFromConnectionString(_options.NorthWindConnection)} database[/][/]")
            .AddColumn(new TableColumn("[cyan]Primary table Foreign table[/]"))
            .AddColumn(new TableColumn("[cyan]Name[/]"))
            .AddColumn(new TableColumn("[cyan]Constraint Name[/]"));
}
