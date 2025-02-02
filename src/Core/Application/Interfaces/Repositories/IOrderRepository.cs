using Application.Models.Queries.Orders;
using Common.Dtos;
using Domain.Entities.Orders;

namespace Application.Interfaces.Repositories;

public interface IOrderRepository : IRepository<OrderEntity>
{
    Task<OrderEntity?> GetByIdAsync(int id);
    Task<OrderEntity?> GetByCustomerIdAsync(int customerId);
    Task<ResultBaseByListDto<OrderEntity>> GetByFilterAsync(OrderFilter filter);
}