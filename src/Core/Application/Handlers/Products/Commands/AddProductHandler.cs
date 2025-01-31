using Application.Helper;
using Application.Interfaces;
using Application.Models.Commands.Products;
using Common.Dtos;
using Common.Extensions;
using MediatR;
using System.Net;

namespace Application.Handlers.Products.Commands;

public class AddProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddProductCommand, ResultDto>
{
    public async Task<ResultDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var productExist = await unitOfWork.Products
            .ExistsAsync(x => x.Name == request.Name);

        if (productExist)
            throw new AppException(HttpStatusCode.BadRequest, "محصول از قبل موجود میباشد");

        var product = request.Create();

        unitOfWork.Products.Add(product);
        await unitOfWork.CommitAsync();

        return new ResultDto();
    }
}