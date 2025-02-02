using Application.Models.Results.Customers;
using Common.Dtos;
using MediatR;

namespace Application.Models.Queries.Customers;

public class GetCustomerByIdQuery : IRequest<ResultDto<CustomerDto>>
{
    public int Id { get; set; }
}