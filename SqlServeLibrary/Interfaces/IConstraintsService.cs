using SqlServerLibrary.Models;

namespace SqlServerLibrary.Interfaces;
public interface IConstraintsService
{

    /// <summary>
    /// Get table constraints for a database
    /// </summary>
    /// <returns></returns>
    public List<TableConstraints> GetAll(string connectionString);
}