using Domain.Entities.Orders;

namespace Application.Helper;

public static class OrderItemHelper
{
    public static OrderItemEntity Create(int productId, int orderId) =>
        new()
        {
            ProductId = productId,
            OrderId = orderId,
            Quantity = 1,
            UnitPrice = 0,

            CreatedTime = DateTimeOffset.UtcNow,
            IsRemoved = false,
        };
}