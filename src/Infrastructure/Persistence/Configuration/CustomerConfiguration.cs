using Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<CustomerEntity>
{
    public void Configure(EntityTypeBuilder<CustomerEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Property(c => c.FullName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(11);

        builder.HasQueryFilter(x => !x.IsRemoved);
    }
}