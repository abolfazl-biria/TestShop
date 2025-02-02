using Application.Models.Queries.OrderItems;
using Common.Extensions;
using Domain.Entities.Orders;

namespace Persistence.Extensions;

public static class OrderItemQueryableExtension
{
    public static IQueryable<OrderItemEntity>
        ApplyFilter(this IQueryable<OrderItemEntity> query, OrderItemFilter filter) =>
        query
            .WhereIf(filter.ProductId.HasValue, x => x.ProductId == filter.ProductId)
            .WhereIf(filter.Quantity.HasValue, x => x.Quantity == filter.Quantity)
            .WhereIf(filter.OrderId.HasValue, x => x.OrderId == filter.OrderId);
}