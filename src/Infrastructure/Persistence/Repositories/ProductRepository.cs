using Application.Interfaces.Repositories;
using Application.Models.Queries.Products;
using Common.Dtos;
using Common.Extensions;
using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.Repositories;

public class ProductRepository : Repository<ProductEntity>, IProductRepository
{
    private readonly IQueryable<ProductEntity> _queryable;

    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
        _queryable = DbContext.Set<ProductEntity>();
    }

    private IQueryable<ProductEntity> Query =>
        _queryable;

    public async Task<ProductEntity?> GetByIdAsync(int id) =>
        await Query.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<ResultBaseByListDto<ProductEntity>> GetByFilterAsync(ProductFilter filter) =>
        await Query.AsNoTracking().ApplyFilter(filter)
            .SortBy(filter.ColumnName ?? nameof(ProductEntity.Id), filter.OrderDir ?? SortingExtension.DirectionEnum.Desc)
            .PaginateAsync(filter.Page, filter.PageSize, filter.Pagination);
}