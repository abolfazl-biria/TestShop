using Application.Models.Results.Orders;
using Common.Dtos;
using MediatR;

namespace Application.Models.Queries.Orders;

public class GetOrderByIdQuery : IRequest<ResultDto<OrderDto>>
{
    public int Id { get; set; }
}