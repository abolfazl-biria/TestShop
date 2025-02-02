using Domain.Entities.Orders;

namespace Application.Helper;

public static class OrderHelper
{
    public static OrderEntity Create(int customerId, int productId) =>
        new()
        {
            Status = OrderStatus.Pending,
            Amount = 0,
            CustomerId = customerId,

            CreatedTime = DateTimeOffset.UtcNow,
            IsRemoved = false,
            OrderItems = [new OrderItemEntity
            {
                ProductId = productId,
                CreatedTime = DateTimeOffset.UtcNow,
                IsRemoved = false,
                Quantity = 1,
                UnitPrice = 0,
            }]
        };

    public static OrderEntity Update(this OrderEntity entity, decimal amount, OrderStatus status)
    {
        entity.Status = status;
        entity.Amount = amount;

        entity.ModifiedDate = DateTimeOffset.UtcNow;

        return entity;
    }

    public static OrderEntity Update(this OrderEntity entity, OrderStatus status)
    {
        entity.Status = status;

        entity.ModifiedDate = DateTimeOffset.UtcNow;

        return entity;
    }
}