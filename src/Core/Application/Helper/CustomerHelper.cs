using Application.Models.Commands.Customers;
using Domain.Entities.Customers;

namespace Application.Helper;

public static class CustomerHelper
{
    public static CustomerEntity Create(this AddCustomerCommand command) =>
        new()
        {
            FullName = command.FullName,
            PhoneNumber = command.PhoneNumber,

            CreatedTime = DateTimeOffset.UtcNow,
            IsRemoved = false,
        };
}