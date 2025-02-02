using Domain.Entities.Orders;

namespace EndPoint.Api.Api.RequestModels.Orders;

public class UpdateOrderRequestDto
{
    public OrderStatus Status { get; set; }
}