using Common.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Common.Dtos;

public abstract class RequestBaseByFilterDto(int page, int pageSize, bool pagination = true) : PaginationDto(page, pageSize, pagination)
{
    [Display(Name = "تاریخ ثبت از")]
    public DateTimeOffset? StartInsertDateTime { get; set; }

    [Display(Name = "تاریخ ثبت تا")]
    public DateTimeOffset? EndInsertDateTime { get; set; }

    [Display(Name = "وضعیت حذف")]
    public bool? IsRemoved { get; set; }

    public SortingExtension.DirectionEnum? OrderDir { get; set; }
    public string? ColumnName { get; set; }
}