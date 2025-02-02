using Application.Models.Results.Orders;
using Common.Dtos;
using MediatR;

namespace Application.Models.Queries.Orders;

public class GetOrdersByFilterQuery(OrderFilter filter) : IRequest<ResultDto<ResultBaseByListDto<OrderDto>>>
{
    public OrderFilter Filter { get; set; } = filter;
}