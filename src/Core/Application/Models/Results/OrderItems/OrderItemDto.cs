using Common.Dtos;
using Domain.Entities.Orders;

namespace Application.Models.Results.OrderItems;

public class OrderItemDto(int id) : ResultBaseDto(id)
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public int OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }

    public int CustomerId { get; set; }
    public string? CustomerFullName { get; set; }
    public string? CustomerPhoneNumber { get; set; }

    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal? ProductPrice { get; set; }
    public int? ProductStockQuantity { get; set; }
}