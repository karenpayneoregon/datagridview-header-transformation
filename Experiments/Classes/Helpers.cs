﻿#nullable enable
using System.Runtime.CompilerServices;

namespace Experiments.Classes
{
    internal class Helpers
    {
        public static void PrintSampleName([CallerMemberName] string? methodName = null)
        {
            AnsiConsole.MarkupLine($"[cyan]Sample:[/] [white]{methodName.SplitCamelCase()}[/]");
            Console.WriteLine();
        }
    }
}
