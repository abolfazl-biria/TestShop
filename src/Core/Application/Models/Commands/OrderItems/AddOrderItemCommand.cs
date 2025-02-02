using Common.Dtos;
using MediatR;

namespace Application.Models.Commands.OrderItems;

public class AddOrderItemCommand : IRequest<ResultDto>
{
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
}