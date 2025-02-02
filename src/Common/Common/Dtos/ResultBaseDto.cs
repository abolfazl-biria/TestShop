using Common.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Common.Dtos;

public abstract class ResultBaseDto<TKey>(TKey id) where TKey : struct
{
    [Display(Name = "شناسه")]
    public string Eid => id.Encode();

    [Display(Name = "تاریخ ثبت")]
    public DateTimeOffset CreatedTime { get; set; }

    [Display(Name = "تاریخ ویرایش")]
    public DateTimeOffset? ModifiedDate { get; set; }

    [Display(Name = "وضعیت حذف")]
    public bool IsRemoved { get; set; }
}

public abstract class ResultBaseDto(int id) : ResultBaseDto<int>(id);