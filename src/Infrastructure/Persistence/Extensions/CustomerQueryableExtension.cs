using Application.Models.Queries.Customers;
using Common.Extensions;
using Domain.Entities.Customers;

namespace Persistence.Extensions;

public static class CustomerQueryableExtension
{
    public static IQueryable<CustomerEntity>
        ApplyFilter(this IQueryable<CustomerEntity> query, CustomerFilter filter) =>
        query
            .WhereIf(!string.IsNullOrWhiteSpace(filter.FullName), x => x.FullName.Contains(filter.FullName!))
            .WhereIf(!string.IsNullOrWhiteSpace(filter.PhoneNumber), x => x.PhoneNumber.Contains(filter.PhoneNumber!));
}