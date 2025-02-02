using Application.Interfaces.Repositories;
using Application.Models.Queries.OrderItems;
using Common.Dtos;
using Common.Extensions;
using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.Repositories;

public class OrderItemRepository : Repository<OrderItemEntity>, IOrderItemRepository
{
    private readonly IQueryable<OrderItemEntity> _queryable;

    public OrderItemRepository(AppDbContext dbContext) : base(dbContext)
    {
        _queryable = DbContext.Set<OrderItemEntity>();
    }

    private IQueryable<OrderItemEntity> Query => _queryable;

    public async Task<OrderItemEntity?> GetByIdForUpdateAsync(int id) =>
        await DbContext.Set<OrderItemEntity>()
            .FromSqlRaw("SELECT * FROM OrderItemEntity WITH (UPDLOCK, ROWLOCK)  WHERE Id = {0}", id)
            .SingleOrDefaultAsync();

    public async Task<OrderItemEntity?> GetByIdAsync(int id) =>
        await Query
            .Include(x => x.Product)
            .Include(x => x.Order).ThenInclude(x => x.Customer)
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<ResultBaseByListDto<OrderItemEntity>> GetByFilterAsync(OrderItemFilter filter) =>
        await Query.AsNoTracking()
            .Include(x => x.Product)
            .Include(x => x.Order).ThenInclude(x => x.Customer)
            .ApplyFilter(filter)
            .SortBy(filter.ColumnName ?? nameof(OrderItemEntity.Id), filter.OrderDir ?? SortingExtension.DirectionEnum.Desc)
            .PaginateAsync(filter.Page, filter.PageSize, filter.Pagination);
}