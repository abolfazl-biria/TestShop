using Application.Models.Results.Customers;
using Common.Dtos;
using MediatR;

namespace Application.Models.Queries.Customers;

public class GetCustomersByFilterQuery(CustomerFilter filter) : IRequest<ResultDto<ResultBaseByListDto<CustomerDto>>>
{
    public CustomerFilter Filter { get; set; } = filter;
}