using Application.Interfaces;
using Application.Models.Commands.Products;
using Common.Dtos;
using Common.Extensions;
using MediatR;
using System.Net;

namespace Application.Handlers.Products.Commands;

public class DeleteProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand, ResultDto>
{
    public async Task<ResultDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetByIdAsync(request.Id)
                      ?? throw new AppException(HttpStatusCode.BadRequest, "محصول یافت نشد");

        product.ModifiedDate = DateTimeOffset.UtcNow;
        product.IsRemoved = true;

        unitOfWork.Products.Update(product);
        await unitOfWork.CommitAsync();

        return new ResultDto();
    }
}