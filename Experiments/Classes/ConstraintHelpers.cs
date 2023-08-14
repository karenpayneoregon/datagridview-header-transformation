using SqlServerLibrary.Models;

namespace Experiments.Classes;
public class ConstraintHelpers
{

    /// <summary>
    /// Delete tables with cascading delete rule
    /// </summary>
    public static void GetTablesWithDeleteRuleForNorthWindDatabase(List<TableConstraints> list)
    {
        ConsoleHelpers.PrintSampleName();

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

    private static Table CreateTable() =>
        new Table()
            .Border(TableBorder.Square)
            .BorderColor(Color.Grey100)
            .Alignment(Justify.Center)
            .Title($"[white][B]Tables with delete constraints for {nameof(ConnectionStrings.NorthWindConnection)} database[/][/]")
            .AddColumn(new TableColumn("[cyan]Primary table Foreign table[/]"))
            .AddColumn(new TableColumn("[cyan]Name[/]"))
            .AddColumn(new TableColumn("[cyan]Constraint Name[/]"));
}