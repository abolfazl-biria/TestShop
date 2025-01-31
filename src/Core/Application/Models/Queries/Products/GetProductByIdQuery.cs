using Application.Models.Results.Products;
using Common.Dtos;
using MediatR;

namespace Application.Models.Queries.Products;

public class GetProductByIdQuery : IRequest<ResultDto<ProductDto>>
{
    public int Id { get; set; }
}