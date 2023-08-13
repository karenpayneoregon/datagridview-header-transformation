namespace Experiments.Interfaces;

public interface IConstraintInformation
{
    /// <summary>
    /// Delete tables with cascading delete rule
    /// </summary>
    void GetTablesWithDeleteRuleForNorthWindDatabase();
}