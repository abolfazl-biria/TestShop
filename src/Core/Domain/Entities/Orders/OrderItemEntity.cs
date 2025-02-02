using Domain.Entities.BaseEntities;
using Domain.Entities.Products;

namespace Domain.Entities.Orders;

public class OrderItemEntity : BaseEntity
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    #region Navigations

    public OrderEntity Order { get; set; }
    public ProductEntity Product { get; set; }

    #endregion
}