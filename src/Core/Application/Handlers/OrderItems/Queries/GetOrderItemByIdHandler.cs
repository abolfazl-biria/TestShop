using Application.Interfaces;
using Application.Mappers;
using Application.Models.Queries.OrderItems;
using Application.Models.Results.OrderItems;
using Common.Dtos;
using Common.Extensions;
using MediatR;
using System.Net;

namespace Application.Handlers.OrderItems.Queries;

public class GetOrderItemByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOrderItemByIdQuery, ResultDto<OrderItemDto>>
{
    public async Task<ResultDto<OrderItemDto>> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.OrderItems.GetByIdAsync(request.Id)
                     ?? throw new AppException(HttpStatusCode.BadRequest, "یافت نشد");

        return new ResultDto<OrderItemDto>(result.MapToModel());
    }
}