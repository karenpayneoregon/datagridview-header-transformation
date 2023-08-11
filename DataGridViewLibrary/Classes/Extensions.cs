using System.Data;

namespace DataGridViewLibrary.Classes;

public static class Extensions
{
    public static string GetStringSafe(this IDataReader pReader, string pField) 
        => ((pReader[pField] is DBNull) ? null : pReader[pField].ToString());
}