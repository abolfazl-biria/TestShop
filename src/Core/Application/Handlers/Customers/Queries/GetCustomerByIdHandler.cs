using Application.Interfaces;
using Application.Mappers;
using Application.Models.Queries.Customers;
using Application.Models.Results.Customers;
using Common.Dtos;
using Common.Extensions;
using MediatR;
using System.Net;

namespace Application.Handlers.Customers.Queries;

public class GetCustomerByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCustomerByIdQuery, ResultDto<CustomerDto>>
{
    public async Task<ResultDto<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Customers.GetByIdAsync(request.Id)
                     ?? throw new AppException(HttpStatusCode.BadRequest, "یافت نشد");

        return new ResultDto<CustomerDto>(result.MapToModel());
    }
}