using Domain.Entities.BaseEntities;
using Domain.Entities.Customers;

namespace Domain.Entities.Orders;

public class OrderEntity : BaseEntity
{
    public int CustomerId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal Amount { get; set; }

    #region Navigations

    public CustomerEntity Customer { get; set; }
    public ICollection<OrderItemEntity> OrderItems { get; set; } = [];

    #endregion
}