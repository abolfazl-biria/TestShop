using Common.Dtos;
using MediatR;

namespace Application.Models.Commands.OrderItems;

public class DeleteOrderItemCommand : IRequest<ResultDto>
{
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
}