using Common.Dtos;

namespace Application.Models.Results.Customers;

public class CustomerDto(int id) : ResultBaseDto(id)
{
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}