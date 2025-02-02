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
        await unitOfWork.BeginTransactionAsync();
        try
        {
            //اعمال lock بر روی دیتا بیس 
            //به جای استفاده از lock در سطح کد
            var entity = await unitOfWork.Products.GetByIdForUpdateAsync(request.Id)
                         ?? throw new AppException(HttpStatusCode.BadRequest, "یافت نشد");

            var entityExist = await unitOfWork.Products
                .ExistsAsync(x => x.Name == request.Name && x.Id != entity.Id);

            if (entityExist)
                throw new AppException(HttpStatusCode.BadRequest, "از قبل موجود میباشد");

            entity.Update(request);

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