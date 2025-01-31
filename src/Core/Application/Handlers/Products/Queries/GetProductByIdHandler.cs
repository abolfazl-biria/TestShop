using Application.Interfaces;
using Application.Mappers;
using Application.Models.Queries.Products;
using Application.Models.Results.Products;
using Common.Dtos;
using Common.Extensions;
using MediatR;
using System.Net;

namespace Application.Handlers.Products.Queries;

public class GetProductByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetProductByIdQuery, ResultDto<ProductDto>>
{
    public async Task<ResultDto<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Products.GetByIdAsync(request.Id)
                     ?? throw new AppException(HttpStatusCode.BadRequest, "محصول یافت نشد");

        return new ResultDto<ProductDto>(result.MapToModel());
    }
}