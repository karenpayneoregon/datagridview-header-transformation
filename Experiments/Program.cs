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

        serviceProvider.GetService<DataOperations>().GetColumnDescriptionsForBooks();
        serviceProvider.GetService<DataOperations>().GetColumnDescriptionsForContacts();

        ExitPrompt();
    }
}