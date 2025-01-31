using Common.Dtos;

namespace Application.Models.Queries.Products;

public class ProductFilter(int page, int pageSize, bool pagination = true) : RequestBaseByFilterDto(page, pageSize, pagination)
{
    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public int? StockQuantity { get; set; }
}