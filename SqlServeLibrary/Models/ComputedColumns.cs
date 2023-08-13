namespace SqlServerLibrary.Models;
/// <summary>
/// For holding information for computed columns
/// </summary>
public class ComputedColumns
{
    public string SchemaName { get; set; }
    public string ColumnName { get; set; }
    public string TableName { get; set; }
    public string DataType { get; set; }
    public string Definition { get; set; }
    public override string ToString() => $"{TableName}.{ColumnName} '{Definition}'";

}
