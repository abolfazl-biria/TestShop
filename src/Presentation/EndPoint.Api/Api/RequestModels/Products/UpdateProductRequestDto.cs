namespace EndPoint.Api.Api.RequestModels.Products;

public class UpdateProductRequestDto
{
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }
}