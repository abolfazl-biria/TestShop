using Application.Models.Commands.Products;
using Domain.Entities.Products;

namespace Application.Helper;

public static class ProductHelper
{
    public static ProductEntity Create(this AddProductCommand command) =>
        new()
        {
            Name = command.Name,
            Price = command.Price,
            StockQuantity = command.StockQuantity,

            CreatedTime = DateTimeOffset.UtcNow,
            IsRemoved = false,
        };

    public static ProductEntity Update(this ProductEntity entity, UpdateProductCommand command)
    {
        entity.Name = command.Name;
        entity.Price = command.Price;
        entity.StockQuantity = command.StockQuantity;

        entity.ModifiedDate = DateTimeOffset.UtcNow;

        return entity;
    }
}