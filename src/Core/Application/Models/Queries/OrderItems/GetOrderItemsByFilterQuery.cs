using Application.Models.Results.OrderItems;
using Common.Dtos;
using MediatR;

namespace Application.Models.Queries.OrderItems;

public class GetOrderItemsByFilterQuery(OrderItemFilter filter) : IRequest<ResultDto<ResultBaseByListDto<OrderItemDto>>>
{
    public OrderItemFilter Filter { get; set; } = filter;
}