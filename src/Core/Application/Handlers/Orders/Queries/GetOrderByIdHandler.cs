using Application.Interfaces;
using Application.Mappers;
using Application.Models.Queries.Orders;
using Application.Models.Results.Orders;
using Common.Dtos;
using Common.Extensions;
using MediatR;
using System.Net;

namespace Application.Handlers.Orders.Queries;

public class GetOrderByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOrderByIdQuery, ResultDto<OrderDto>>
{
    public async Task<ResultDto<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Orders.GetByIdAsync(request.Id)
                     ?? throw new AppException(HttpStatusCode.BadRequest, "یافت نشد");

        return new ResultDto<OrderDto>(result.MapToModel());
    }
}