using Application.Interfaces;
using Application.Mappers;
using Application.Models.Queries.Products;
using Application.Models.Results.Products;
using Common.Dtos;
using MediatR;

namespace Application.Handlers.Products.Queries;

public class GetProductsByFilterHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetProductsByFilterQuery, ResultDto<ResultBaseByListDto<ProductDto>>>
{
    public async Task<ResultDto<ResultBaseByListDto<ProductDto>>> Handle(GetProductsByFilterQuery request,
        CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Products.GetByFilterAsync(request.Filter);

        return new ResultDto<ResultBaseByListDto<ProductDto>>(
            new ResultBaseByListDto<ProductDto>(result.Result?.MapToModel(), result.Count, result.PageCount));
    }
}