using SqlServerLibrary.Models;

namespace SqlServerLibrary.Interfaces;

public interface IColumnsService
{
    /// <summary>
    /// Get all table that have computed columns
    /// </summary>
    /// <param name="connectionString">database connection string</param>
    /// <returns>A list of computed columns or an empty list</returns>
    List<ComputedColumns> GetComputedColumnsList(string connectionString);
    List<DateTimeContainer> GetDateTimeColumns(string connectionString);
    List<ColumnDescriptions> ForBooks(string connectionString);
    List<ColumnDescriptions> Contacts(string connectionString);
}