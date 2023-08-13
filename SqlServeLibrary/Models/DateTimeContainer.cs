using SqlServerLibrary.Classes;
namespace SqlServerLibrary.Models;
/// <summary>
/// Represents data returned from <seealso cref="SqlStatements.GetAllDateTimeColumnsInDatabase"/>
/// </summary>
public class DateTimeContainer
{
    public string TableName { get; set; }
    public int ColumnId { get; set; }
    public string ColumnName { get; set; }
    public string DataType { get; set; }
    public byte Scale { get; set; }

    public override string ToString() => $"{TableName}, {ColumnName}";

}
