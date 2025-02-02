using Domain.Entities.BaseEntities;
using Domain.Entities.Orders;

namespace Domain.Entities.Products;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    #region Navigations

    public ICollection<OrderItemEntity> OrderItems { get; set; } = [];

    #endregion
}