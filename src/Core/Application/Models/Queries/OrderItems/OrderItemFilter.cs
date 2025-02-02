using Common.Dtos;

namespace Application.Models.Queries.OrderItems;

public class OrderItemFilter(int page, int pageSize, bool pagination = true) : RequestBaseByFilterDto(page, pageSize, pagination)
{
    public int? OrderId { get; set; }
    public int? ProductId { get; set; }
    public int? Quantity { get; set; }
}