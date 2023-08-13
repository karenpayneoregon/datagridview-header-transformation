#nullable enable
using System.Runtime.CompilerServices;

namespace Experiments.Classes;

/// <summary>
/// Console helpers
/// </summary>
internal class Helpers
{
    /// <summary>
    /// Used to display current method name
    /// </summary>
    /// <param name="methodName"></param>
    public static void PrintSampleName([CallerMemberName] string? methodName = null)
    {
        AnsiConsole.MarkupLine($"[cyan]Sample:[/] [white]{methodName.SplitCamelCase()}[/]");
        Console.WriteLine();
    }
}