using static System.Text.RegularExpressions.Regex;

namespace Experiments.Classes;


public static class StringExtensions
{

    /// <summary>
    /// Split string on uppercase characters and insert a space
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public static string SplitCamelCase(this string sender) 
        => string.Join(" ", Matches(sender, @"([A-Z][a-z]+)").Select(m => m.Value));

    /// <summary>
    /// For Spectre.Console which sees brackets in [dbo].[Contacts] as colors
    /// </summary>
    public static string RemoveDoubleQuotes(this string sender) 
        => sender.Replace("[", "").Replace("]", "");
}
