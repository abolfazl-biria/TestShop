using Application.Models.Results.Customers;
using Domain.Entities.Customers;

namespace Application.Mappers;

public static class CustomerMapper
{
    public static CustomerDto MapToModel(this CustomerEntity entity) =>
        new(entity.Id)
        {
            FullName = entity.FullName,
            PhoneNumber = entity.PhoneNumber,

            CreatedTime = entity.CreatedTime,
            ModifiedDate = entity.ModifiedDate,
            IsRemoved = entity.IsRemoved,
        };

    public static List<CustomerDto> MapToModel(this IList<CustomerEntity> entities) =>
        entities.Select(entity => entity.MapToModel()).ToList();
}