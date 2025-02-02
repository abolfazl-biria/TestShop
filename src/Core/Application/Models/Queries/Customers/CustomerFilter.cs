using Common.Dtos;

namespace Application.Models.Queries.Customers;

public class CustomerFilter(int page, int pageSize, bool pagination = true) : RequestBaseByFilterDto(page, pageSize, pagination)
{
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
}