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

    public static ProductEntity Update(this ProductEntity product, UpdateProductCommand command)
    {
        product.Name = command.Name;
        product.Price = command.Price;
        product.StockQuantity = command.StockQuantity;

        product.ModifiedDate = DateTimeOffset.UtcNow;

        return product;
    }
}