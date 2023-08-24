using Microsoft.Data.SqlClient;
using SqlServerLibrary.Classes;

namespace SqlServerLibrary.Extensions;

/// <summary>
/// Create dynamic WHERE IN command text
/// </summary>
public static class SqlClientParamBuilder
{
    /// <summary>
    /// Creates parameters for WHERE IN clause
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="commandText">SELECT partly built up to WHERE IN ({0})</param>
    /// <param name="prefix">Prefix for parameter names</param>
    /// <param name="parameters">Parameter values</param>
    /// <returns></returns>
    private static string BuildWhereInCommandText<T>(string commandText, string prefix, IEnumerable<T> parameters)
    {
        string[] parameterNames = parameters.Select((paramText, paramNumber) => $"@{prefix}{paramNumber}").ToArray();
        var inClause = string.Join(",", parameterNames);
        var whereInClause = string.Format(commandText.Trim(), inClause);

        return whereInClause;
    }

    /// <summary>
    /// Populate parameter values
    /// Take each value in
    /// </summary>
    /// <typeparam name="T">Values to place into parameters</typeparam>
    /// <param name="cmd">Valid command object</param>
    /// <param name="commandText"></param>
    /// <param name="prefix">Prefix for parameter names</param>
    /// <param name="parameters">Parameter values</param>
    public static void WhereInParameters<T>(this SqlCommand cmd, string commandText, string prefix, IEnumerable<T> parameters)
    {
        cmd.CommandText = BuildWhereInCommandText(commandText, prefix, parameters);
        string[] parameterValues = parameters.Select((paramText) => paramText.ToString()).ToArray();
        string[] parameterNames = parameterValues.Select((paramText, paramNumber) => $"@{prefix}{paramNumber}").ToArray();

        for (int index = 0; index < parameterNames.Length; index++)
        {
            cmd.Parameters.Add(new SqlParameter()
            {
                ParameterName = parameterNames[index],
                SqlDbType = SqlTypeHelper.GetDatabaseType(typeof(T)),
                Value = parameterValues[index]
            });
        }
    }
}

