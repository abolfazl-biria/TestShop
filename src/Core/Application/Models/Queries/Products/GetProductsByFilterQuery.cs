using Application.Models.Results.Products;
using Common.Dtos;
using MediatR;

namespace Application.Models.Queries.Products;

public class GetProductsByFilterQuery(ProductFilter filter) : IRequest<ResultDto<ResultBaseByListDto<ProductDto>>>
{
    public ProductFilter Filter { get; set; } = filter;
}