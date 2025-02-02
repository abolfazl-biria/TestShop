using Application.Interfaces;
using Application.Mappers;
using Application.Models.Queries.OrderItems;
using Application.Models.Results.OrderItems;
using Common.Dtos;
using MediatR;

namespace Application.Handlers.OrderItems.Queries;

public class GetOrderItemsByFilterHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetOrderItemsByFilterQuery, ResultDto<ResultBaseByListDto<OrderItemDto>>>
{
    public async Task<ResultDto<ResultBaseByListDto<OrderItemDto>>> Handle(GetOrderItemsByFilterQuery request,
        CancellationToken cancellationToken)
    {
        var result = await unitOfWork.OrderItems.GetByFilterAsync(request.Filter);

        return new ResultDto<ResultBaseByListDto<OrderItemDto>>(
            new ResultBaseByListDto<OrderItemDto>(result.Result?.MapToModel(), result.Count, result.PageCount));
    }
}