using Application.Models.Queries.Orders;
using Common.Extensions;
using Domain.Entities.Orders;

namespace Persistence.Extensions;

public static class OrderQueryableExtension
{
    public static IQueryable<OrderEntity>
        ApplyFilter(this IQueryable<OrderEntity> query, OrderFilter filter) =>
        query
            .WhereIf(filter.Status.HasValue, x => x.Status == filter.Status)
            .WhereIf(filter.CustomerId.HasValue, x => x.CustomerId == filter.CustomerId);
}