using Application.Interfaces.Repositories;
using Application.Models.Queries.Customers;
using Common.Dtos;
using Common.Extensions;
using Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.Repositories;

public class CustomerRepository : Repository<CustomerEntity>, ICustomerRepository
{
    private readonly IQueryable<CustomerEntity> _queryable;

    public CustomerRepository(AppDbContext dbContext) : base(dbContext)
    {
        _queryable = DbContext.Set<CustomerEntity>();
    }

    private IQueryable<CustomerEntity> Query => _queryable;

    public async Task<CustomerEntity?> GetByIdAsync(int id) =>
        await Query.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<ResultBaseByListDto<CustomerEntity>> GetByFilterAsync(CustomerFilter filter) =>
        await Query.AsNoTracking().ApplyFilter(filter)
            .SortBy(filter.ColumnName ?? nameof(CustomerEntity.Id), filter.OrderDir ?? SortingExtension.DirectionEnum.Desc)
            .PaginateAsync(filter.Page, filter.PageSize, filter.Pagination);
}