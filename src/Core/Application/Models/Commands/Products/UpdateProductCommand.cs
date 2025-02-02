using Common.Dtos;
using MediatR;

namespace Application.Models.Commands.Products;

public class UpdateProductCommand : IRequest<ResultDto>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }
}