using Application.Models.Results.OrderItems;
using Application.Models.Results.Orders;
using Domain.Entities.Orders;

namespace Application.Mappers;

public static class OrderMapper
{
    public static OrderDto MapToModel(this OrderEntity entity) =>
        new(entity.Id)
        {
            CustomerId = entity.CustomerId,
            Status = entity.Status,

            CustomerFullName = entity.Customer.FullName,
            CustomerPhoneNumber = entity.Customer.PhoneNumber,

            OrderItems = entity.OrderItems.Where(x => !x.IsRemoved).Select(x => new OrderItemDto(x.Id)
            {
                UnitPrice = x.Quantity * x.Product.Price,
                Quantity = x.Quantity,

                CustomerId = entity.CustomerId,
                CustomerFullName = entity.Customer.FullName,
                CustomerPhoneNumber = entity.Customer.PhoneNumber,

                OrderId = entity.Id,
                OrderStatus = entity.Status,

                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                ProductStockQuantity = x.Product.StockQuantity,
                ProductPrice = x.Product.Price,

                CreatedTime = x.CreatedTime,
                ModifiedDate = x.ModifiedDate,
                IsRemoved = x.IsRemoved,
            }).ToList(),

            CreatedTime = entity.CreatedTime,
            ModifiedDate = entity.ModifiedDate,
            IsRemoved = entity.IsRemoved,
        };

    public static List<OrderDto> MapToModel(this IList<OrderEntity> entities) =>
        entities.Select(entity => entity.MapToModel()).ToList();
}