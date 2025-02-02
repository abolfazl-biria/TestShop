using Common.Dtos;
using Domain.Entities.Orders;

namespace Application.Models.Queries.Orders;

public class OrderFilter(int page, int pageSize, bool pagination = true) : RequestBaseByFilterDto(page, pageSize, pagination)
{
    public int? CustomerId { get; set; }
    public OrderStatus? Status { get; set; }
}