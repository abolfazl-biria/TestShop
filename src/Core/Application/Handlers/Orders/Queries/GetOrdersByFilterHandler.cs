using Application.Interfaces;
using Application.Mappers;
using Application.Models.Queries.Orders;
using Application.Models.Results.Orders;
using Common.Dtos;
using MediatR;

namespace Application.Handlers.Orders.Queries;

public class GetOrdersByFilterHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetOrdersByFilterQuery, ResultDto<ResultBaseByListDto<OrderDto>>>
{
    public async Task<ResultDto<ResultBaseByListDto<OrderDto>>> Handle(GetOrdersByFilterQuery request,
        CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Orders.GetByFilterAsync(request.Filter);

        return new ResultDto<ResultBaseByListDto<OrderDto>>(
            new ResultBaseByListDto<OrderDto>(result.Result?.MapToModel(), result.Count, result.PageCount));
    }
}