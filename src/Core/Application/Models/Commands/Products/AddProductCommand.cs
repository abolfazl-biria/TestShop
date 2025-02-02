using Common.Dtos;
using MediatR;

namespace Application.Models.Commands.Products;

public class AddProductCommand : IRequest<ResultDto>
{
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }
}