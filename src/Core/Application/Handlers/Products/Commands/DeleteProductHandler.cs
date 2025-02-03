using Application.Interfaces;
using Application.Models.Commands.Products;
using Common.Dtos;
using Common.Extensions;
using MediatR;
using System.Net;

namespace Application.Handlers.Products.Commands;

public class DeleteProductHandle(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand, ResultDto>
{
    public async Task<ResultDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            var entity = await unitOfWork.Products.GetByIdAsync(request.Id)
                         ?? throw new AppException(HttpStatusCode.BadRequest, "یافت نشد");

            entity.ModifiedDate = DateTimeOffset.UtcNow;
            entity.IsRemoved = true;

            unitOfWork.Products.Update(entity);

            await unitOfWork.CommitAsync();

            return new ResultDto();
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}