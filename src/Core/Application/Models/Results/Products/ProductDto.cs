using Common.Dtos;

namespace Application.Models.Results.Products;

public class ProductDto(int id) : ResultBaseDto(id)
{
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }
}