using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

        builder.HasQueryFilter(x => !x.IsRemoved);

        builder.Property(oi => oi.Quantity).IsRequired();
        builder.Property(oi => oi.UnitPrice).IsRequired();

        #region Navigations

        builder
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}