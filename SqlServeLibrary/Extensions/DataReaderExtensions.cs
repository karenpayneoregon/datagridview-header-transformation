using System.Data;

namespace SqlServerLibrary.Extensions;

public static class DataReaderExtensions
{
    /// <summary>
    /// Read column of type string assert for <see cref="DBNull"/>
    /// </summary>
    public static string GetStringSafe(this IDataReader reader, string column)
        => reader[column] is DBNull ?
            null :
            reader[column].ToString();
}