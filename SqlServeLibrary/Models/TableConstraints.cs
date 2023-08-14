using SqlServerLibrary.Classes;

namespace SqlServerLibrary.Models;

/// <summary>
/// Represents a row from <see cref="ConstraintsService.GetAll"/>
/// </summary>
public class TableConstraints
{
    public string PrimaryKeyTable { get; set; }
    public string ConstraintName { get; set; }
    public string PrimaryKeyColumn { get; set; }
    public string ForeignKeyTable { get; set; }
    public string ForeignKeyColumn { get; set; }
    public string UpdateRule { get; set; }
    public string DeleteRule { get; set; }
    public override string ToString() => PrimaryKeyTable;
}
