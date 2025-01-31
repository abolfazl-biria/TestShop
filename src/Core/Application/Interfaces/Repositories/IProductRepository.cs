using Application.Models.Queries.Products;
using Common.Dtos;
using Domain.Entities.Products;

namespace Application.Interfaces.Repositories;

public interface IProductRepository : IRepository<ProductEntity>
{
    Task<ProductEntity?> GetByIdAsync(int id);
    Task<ResultBaseByListDto<ProductEntity>> GetByFilterAsync(ProductFilter filter);
}