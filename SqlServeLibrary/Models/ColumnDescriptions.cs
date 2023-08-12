
namespace SqlServerLibrary.Models;

/// <summary>
/// Container for use in <see cref="Classes.ColumnOperations"/>
/// </summary>
public class ColumnDescriptions
{
    /// <summary>
    /// Name of column
    /// </summary>
    /// <returns></returns>
    public string Name { get; set; }
    /// <summary>
    /// Ordinal position of column
    /// </summary>
    /// <returns></returns>
    public int Ordinal { get; set; }
    /// <summary>
    /// Description of column
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// May be NULL
    /// </remarks>
    public string Description { get; set; }

}