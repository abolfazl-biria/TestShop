using Application.Models.Queries.Products;
using Common.Extensions;
using Domain.Entities.Products;

namespace Persistence.Extensions;

public static class ProductQueryableExtension
{
    public static IQueryable<ProductEntity>
        ApplyFilter(this IQueryable<ProductEntity> query, ProductFilter filter) =>
        query
            .WhereIf(filter.Price.HasValue, x => x.Price == filter.Price)
            .WhereIf(filter.StockQuantity.HasValue, x => x.StockQuantity == filter.StockQuantity)
            .WhereIf(!string.IsNullOrWhiteSpace(filter.Name), x => x.Name.Contains(filter.Name!));
}