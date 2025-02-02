using Application.Models.Results.OrderItems;
using Domain.Entities.Orders;

namespace Application.Mappers;

public static class OrderItemMapper
{
    public static OrderItemDto MapToModel(this OrderItemEntity entity) =>
        new(entity.Id)
        {
            UnitPrice = entity.Quantity * entity.Product.Price,
            Quantity = entity.Quantity,

            CustomerId = entity.Order.CustomerId,
            CustomerFullName = entity.Order.Customer.FullName,
            CustomerPhoneNumber = entity.Order.Customer.PhoneNumber,

            OrderId = entity.OrderId,
            OrderStatus = entity.Order.Status,

            ProductId = entity.ProductId,
            ProductName = entity.Product.Name,
            ProductStockQuantity = entity.Product.StockQuantity,
            ProductPrice = entity.Product.Price,

            CreatedTime = entity.CreatedTime,
            ModifiedDate = entity.ModifiedDate,
            IsRemoved = entity.IsRemoved,
        };

    public static List<OrderItemDto> MapToModel(this IList<OrderItemEntity> entities) =>
        entities.Select(entity => entity.MapToModel()).ToList();
}