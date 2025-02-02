using Microsoft.EntityFrameworkCore;

namespace Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Apply Configurations
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(builder);
    }
}