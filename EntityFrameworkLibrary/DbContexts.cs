using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace EntityFrameworkLibrary;

public static class DbContexts
{

    /// <summary>
    /// Find by one or more entities by their primary key
    /// </summary>
    /// <typeparam name="T">Model</typeparam>
    /// <param name="dbContext">DbContext</param>
    /// <param name="keyValues">One or more keys</param>
    /// <code>
    /// await using var context = new NorthWindContext();
    /// var keys = new object[] {1, 2, 3, 4};
    /// var results = await context.FindAllAsync&lt;Category&gt;(keys);
    /// </code>
    public static Task<T[]> WhereInAsync<T>(this DbContext dbContext, params object[] keyValues) where T : class
    {
        IEntityType? entityType = dbContext.Model.FindEntityType(typeof(T));
        var primaryKey = entityType!.FindPrimaryKey();
        var primaryKeyProperty = primaryKey.Properties[0];

        var pkMemberInfo = typeof(T).GetProperty(primaryKeyProperty.Name);

        var parameter = Expression.Parameter(typeof(T), "e");
        var body = Expression.Call(null, ContainsMethod!,
            Expression.Constant(keyValues),
            Expression.Convert(Expression.MakeMemberAccess(parameter, pkMemberInfo!), typeof(object)));
        var predicateExpression = Expression.Lambda<Func<T, bool>>(body, parameter);

        return dbContext.Set<T>().Where(predicateExpression).ToArrayAsync();
    }

    private static readonly MethodInfo? ContainsMethod = typeof(Enumerable)
        .GetMethods()
        .FirstOrDefault(methodInfo => methodInfo.Name == "Contains" && methodInfo.GetParameters().Length == 2)?
        .MakeGenericMethod(typeof(object));

    /// <summary>
    /// Convert T[] to object[]
    /// </summary>
    /// <typeparam name="T">type to convert</typeparam>
    /// <param name="sender">array</param>
    /// <returns>object array</returns>
    public static object[] ToObjectArray<T>(this T[] sender) 
        => sender.OfType<object>().ToArray();
    public static object[] ToObjectArray<T>(this List<T> sender)
        => sender.OfType<object>().ToArray();
}