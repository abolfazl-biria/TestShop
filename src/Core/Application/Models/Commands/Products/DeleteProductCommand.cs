using Common.Dtos;
using MediatR;

namespace Application.Models.Commands.Products;

public class DeleteProductCommand : IRequest<ResultDto>
{
    public int Id { get; set; }
}