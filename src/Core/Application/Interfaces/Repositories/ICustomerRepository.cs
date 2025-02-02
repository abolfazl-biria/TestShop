using Application.Models.Queries.Customers;
using Common.Dtos;
using Domain.Entities.Customers;

namespace Application.Interfaces.Repositories;

public interface ICustomerRepository : IRepository<CustomerEntity>
{
    Task<CustomerEntity?> GetByIdAsync(int id);
    Task<ResultBaseByListDto<CustomerEntity>> GetByFilterAsync(CustomerFilter filter);
}