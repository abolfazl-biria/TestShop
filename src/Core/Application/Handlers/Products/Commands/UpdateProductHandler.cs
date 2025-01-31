using Application.Helper;
using Application.Interfaces;
using Application.Models.Commands.Products;
using Common.Dtos;
using Common.Extensions;
using MediatR;
using System.Net;

namespace Application.Handlers.Products.Commands;

public class UpdateProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand, ResultDto>
{
    public async Task<ResultDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetByIdAsync(request.Id)
                      ?? throw new AppException(HttpStatusCode.BadRequest, "محصول یافت نشد");

        var productExist = await unitOfWork.Products
            .ExistsAsync(x => x.Name == request.Name && x.Id != product.Id);

        if (productExist)
            throw new AppException(HttpStatusCode.BadRequest, "محصول از قبل موجود میباشد");

        product.Update(request);

        unitOfWork.Products.Update(product);
        await unitOfWork.CommitAsync();

        return new ResultDto();
    }
}