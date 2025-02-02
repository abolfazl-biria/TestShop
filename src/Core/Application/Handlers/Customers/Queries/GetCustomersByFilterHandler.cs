using Application.Interfaces;
using Application.Mappers;
using Application.Models.Queries.Customers;
using Application.Models.Results.Customers;
using Common.Dtos;
using MediatR;

namespace Application.Handlers.Customers.Queries;

public class GetCustomersByFilterHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetCustomersByFilterQuery, ResultDto<ResultBaseByListDto<CustomerDto>>>
{
    public async Task<ResultDto<ResultBaseByListDto<CustomerDto>>> Handle(GetCustomersByFilterQuery request,
        CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Customers.GetByFilterAsync(request.Filter);

        return new ResultDto<ResultBaseByListDto<CustomerDto>>(
            new ResultBaseByListDto<CustomerDto>(result.Result?.MapToModel(), result.Count, result.PageCount));
    }
}