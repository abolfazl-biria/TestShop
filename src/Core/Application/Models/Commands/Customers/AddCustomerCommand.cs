using Common.Dtos;
using MediatR;

namespace Application.Models.Commands.Customers;

public class AddCustomerCommand : IRequest<ResultDto>
{
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}