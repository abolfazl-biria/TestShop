using Microsoft.EntityFrameworkCore;
using Persistence;

namespace EndPoint.Api.Extensions.DependencyInjection;

public static class DataBaseInjection
{
    public static IServiceCollection AddConfiguredDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContext<AppDbContext>(option => option
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    providerOptions =>
                    {
                        providerOptions.CommandTimeout(int.MaxValue); //Timeout in seconds
                    }));

        return services;
    }
}