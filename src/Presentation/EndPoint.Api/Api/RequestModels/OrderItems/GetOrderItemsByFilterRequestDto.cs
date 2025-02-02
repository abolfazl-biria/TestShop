using Common.Extensions;

namespace EndPoint.Api.Api.RequestModels.OrderItems;

public class GetOrderItemsByFilterRequestDto
{
    public string? OrderEid { get; set; }
    public string? ProductEid { get; set; }
    public int? Quantity { get; set; }

    public int PageSize { get; set; }
    public int Page { get; set; }
    public bool Pagination { get; set; } = true;


    public DateTimeOffset? StartInsertDateTime { get; set; }

    public DateTimeOffset? EndInsertDateTime { get; set; }

    public bool? IsRemoved { get; set; }

    public SortingExtension.DirectionEnum? OrderDir { get; set; }
    public string? ColumnName { get; set; }
}