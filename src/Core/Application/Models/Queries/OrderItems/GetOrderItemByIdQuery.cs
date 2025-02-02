using Application.Models.Results.OrderItems;
using Common.Dtos;
using MediatR;

namespace Application.Models.Queries.OrderItems;

public class GetOrderItemByIdQuery : IRequest<ResultDto<OrderItemDto>>
{
    public int Id { get; set; }
}