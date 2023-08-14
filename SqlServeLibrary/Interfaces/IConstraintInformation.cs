namespace SqlServerLibrary.Interfaces;

public interface IConstraintInformation
{
    /// <summary>
    /// Delete tables with cascading delete rule
    /// </summary>
    public void GetTablesWithDeleteRuleForNorthWindDatabase();
}