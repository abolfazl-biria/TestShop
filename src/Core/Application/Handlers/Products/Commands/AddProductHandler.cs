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
        var entityExist = await unitOfWork.Products
            .ExistsAsync(x => x.Name == request.Name);

        if (entityExist)
            throw new AppException(HttpStatusCode.BadRequest, "از قبل موجود میباشد");

        var entity = request.Create();

        unitOfWork.Products.Add(entity);
        await unitOfWork.SaveChangesAsync();

        return new ResultDto();
    }
}