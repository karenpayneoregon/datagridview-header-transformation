using Experiments.Classes;
using Microsoft.Extensions.DependencyInjection;
using static Experiments.Classes.SpectreConsoleHelpers;

namespace Experiments;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        var services = Utilities.ConfigureServices();
        await using var serviceProvider = services.BuildServiceProvider();

        AnsiConsole.MarkupLine("[cyan]Table descriptions from two different databases[/]");
        serviceProvider.GetService<ColumnInformation>().ForBooks();
        serviceProvider.GetService<ColumnInformation>().Contacts();
        serviceProvider.GetService<ColumnInformation>().GetComputedColumns();

        ExitPrompt();
    }
}