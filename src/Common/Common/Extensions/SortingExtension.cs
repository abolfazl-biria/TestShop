using System.Linq.Expressions;

namespace Common.Extensions;

public static class SortingExtension
{
    public enum DirectionEnum
    {
        Asc,
        Desc
    }
    public static IQueryable<T> SortBy<T>(this IQueryable<T> query, string propertyName, DirectionEnum direction, IComparer<T>? comparer = null)
    {
        return direction switch
        {
            DirectionEnum.Asc => ApplyOrdering(query, propertyName, "OrderBy", comparer),
            DirectionEnum.Desc => ApplyOrdering(query, propertyName, "OrderByDescending", comparer),
            _ => query
        };
    }

    public static IQueryable<T> ThenBy<T>(this IQueryable<T> query, string propertyName, DirectionEnum direction, IComparer<T>? comparer = null)
    {
        return direction switch
        {
            DirectionEnum.Asc => ApplyOrdering(query, propertyName, "ThenBy", comparer),
            DirectionEnum.Desc => ApplyOrdering(query, propertyName, "ThenByDescending", comparer),
            _ => query
        };
    }

    private static IQueryable<T> ApplyOrdering<T>(IQueryable<T> query, string propertyName, string methodName, IComparer<T>? comparer = null)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var propertyExpression = propertyName.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);
        var lambda = Expression.Lambda(propertyExpression, param);

        var methodCall = comparer != null
            ? Expression.Call(typeof(Queryable), methodName, new[] { typeof(T), propertyExpression.Type }, query.Expression, lambda, Expression.Constant(comparer))
            : Expression.Call(typeof(Queryable), methodName, new[] { typeof(T), propertyExpression.Type }, query.Expression, lambda);

        return query.Provider.CreateQuery<T>(methodCall);
    }
}