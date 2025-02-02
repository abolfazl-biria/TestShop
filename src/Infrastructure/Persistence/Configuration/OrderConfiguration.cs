using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

        builder.HasIndex(x => new { x.Status, x.CustomerId }).IsUnique().HasFilter($"{nameof(OrderEntity.IsRemoved)} = 0");

        builder.HasQueryFilter(x => !x.IsRemoved);

        #region Navigations

        builder
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}