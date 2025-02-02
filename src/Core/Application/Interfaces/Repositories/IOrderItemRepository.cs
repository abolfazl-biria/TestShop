using Application.Models.Queries.OrderItems;
using Common.Dtos;
using Domain.Entities.Orders;

namespace Application.Interfaces.Repositories;

public interface IOrderItemRepository : IRepository<OrderItemEntity>
{
    Task<OrderItemEntity?> GetByIdForUpdateAsync(int id);
    Task<OrderItemEntity?> GetByIdAsync(int id);
    Task<ResultBaseByListDto<OrderItemEntity>> GetByFilterAsync(OrderItemFilter filter);
}