using Common.Dtos;
using Domain.Entities.Orders;
using MediatR;

namespace Application.Models.Commands.Orders;

public class UpdateOrderCommand : IRequest<ResultDto>
{
    public int CustomerId { get; set; }
    public OrderStatus Status { get; set; }
}