using Domain.Entities.BaseEntities;
using Domain.Entities.Orders;

namespace Domain.Entities.Customers;

public class CustomerEntity : BaseEntity
{
    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    #region Navigations

    public virtual ICollection<OrderEntity> Orders { get; set; } = [];

    #endregion
}