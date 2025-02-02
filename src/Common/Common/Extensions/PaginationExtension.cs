using Common.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Common.Extensions;

public static class PaginationExtension
{
    public static ResultBaseByListDto<T> Paginate<T>(this IQueryable<T> query, int page = 0, int pageSize = 10, bool pagination = true)
        where T : class =>
        pagination ? PaginateQuery(query, page, pageSize) : PaginateQuery(query);

    private static ResultBaseByListDto<T> PaginateQuery<T>(IQueryable<T> query) where T : class
    {
        var result = query.ToList();
        var count = result.Count;
        return new ResultBaseByListDto<T>(result, count, count > 0 ? 1 : 0);
    }

    private static ResultBaseByListDto<T> PaginateQuery<T>(IQueryable<T> query, int page, int pageSize) where T : class
    {
        var count = query.Count();
        var results = query.Skip(page * pageSize).Take(pageSize).ToList();
        return new ResultBaseByListDto<T>(results, count, GetPageCount(count, pageSize));
    }


    public static async Task<ResultBaseByListDto<T>> PaginateAsync<T>(this IQueryable<T> query, int page = 0, int pageSize = 10, bool pagination = true, CancellationToken cancellationToken = default)
        where T : class =>
        pagination ? await PaginateQueryAsync(query, page, pageSize, cancellationToken) : await PaginateQueryAsync(query, cancellationToken);

    private static async Task<ResultBaseByListDto<T>> PaginateQueryAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) where T : class
    {
        var result = await query.ToListAsync(cancellationToken);
        var count = result.Count;
        return new ResultBaseByListDto<T>(result, count, count > 0 ? 1 : 0);
    }

    private static async Task<ResultBaseByListDto<T>> PaginateQueryAsync<T>(IQueryable<T> query, int page, int pageSize, CancellationToken cancellationToken) where T : class
    {
        var count = await query.CountAsync(cancellationToken);
        var results = await query.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return new ResultBaseByListDto<T>(results, count, GetPageCount(count, pageSize));
    }

    public static int GetPageCount(int count, int pageSize) => (int)Math.Ceiling((double)count / pageSize);
}