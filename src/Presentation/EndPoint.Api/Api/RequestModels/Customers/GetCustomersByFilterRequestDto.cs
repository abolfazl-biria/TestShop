using Common.Extensions;

namespace EndPoint.Api.Api.RequestModels.Customers;

public class GetCustomersByFilterRequestDto
{
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }

    public int PageSize { get; set; }
    public int Page { get; set; }
    public bool Pagination { get; set; } = true;


    public DateTimeOffset? StartInsertDateTime { get; set; }

    public DateTimeOffset? EndInsertDateTime { get; set; }

    public bool? IsRemoved { get; set; }

    public SortingExtension.DirectionEnum? OrderDir { get; set; }
    public string? ColumnName { get; set; }
}