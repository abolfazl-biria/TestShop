using Application.Models.Results.OrderItems;
using Common.Dtos;
using Domain.Entities.Orders;

namespace Application.Models.Results.Orders;

public class OrderDto(int id) : ResultBaseDto(id)
{
    public int CustomerId { get; set; }
    public OrderStatus Status { get; set; }


    public string CustomerFullName { get; set; } = null!;
    public string CustomerPhoneNumber { get; set; } = null!;

    public List<OrderItemDto> OrderItems { get; set; } = [];
}