using System.Linq.Expressions;

namespace Common.Extensions;

public static class QueryableExtension
{

    public static IQueryable<TSource> WhereIf<TSource>(
        this IQueryable<TSource> source,
        bool condition,
        Expression<Func<TSource, bool>> predicate) =>
        condition ? source.Where(predicate) : source;

    public static IEnumerable<TSource> WhereIf<TSource>(
        this IEnumerable<TSource> source,
        bool condition,
        Func<TSource, bool> predicate) =>
        condition ? source.Where(predicate) : source;
}