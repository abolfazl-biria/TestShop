using Application.Models.Results.Products;
using Domain.Entities.Products;

namespace Application.Mappers;

public static class ProductMapper
{
    public static ProductDto MapToModel(this ProductEntity entity) =>
        new(entity.Id)
        {
            Name = entity.Name,
            StockQuantity = entity.StockQuantity,
            Price = entity.Price,

            CreatedTime = entity.CreatedTime,
            ModifiedDate = entity.ModifiedDate,
            IsRemoved = entity.IsRemoved,
        };

    public static List<ProductDto> MapToModel(this IList<ProductEntity> entities) =>
        entities.Select(entity => entity.MapToModel()).ToList();
}