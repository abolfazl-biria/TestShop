using Application.Interfaces.Repositories;
using Application.Models.Queries.Orders;
using Common.Dtos;
using Common.Extensions;
using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.Repositories;

public class OrderRepository : Repository<OrderEntity>, IOrderRepository
{
    private readonly IQueryable<OrderEntity> _queryable;

    public OrderRepository(AppDbContext dbContext) : base(dbContext)
    {
        _queryable = DbContext.Set<OrderEntity>();
    }

    public async Task<OrderEntity?> GetByIdAsync(int id) =>
        await _queryable
            .Include(x => x.Customer)
            .Include(x => x.OrderItems).ThenInclude(x => x.Product)
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<OrderEntity?> GetByCustomerIdAsync(int customerId) =>
        await _queryable
            .Include(x => x.OrderItems).ThenInclude(x => x.Product)
            .SingleOrDefaultAsync(x => x.CustomerId == customerId && x.Status == OrderStatus.Pending);

    public async Task<ResultBaseByListDto<OrderEntity>> GetByFilterAsync(OrderFilter filter) =>
        await _queryable.AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.OrderItems).ThenInclude(x => x.Product)
            .ApplyFilter(filter)
            .SortBy(filter.ColumnName ?? nameof(OrderEntity.Id), filter.OrderDir ?? SortingExtension.DirectionEnum.Desc)
            .PaginateAsync(filter.Page, filter.PageSize, filter.Pagination);
}