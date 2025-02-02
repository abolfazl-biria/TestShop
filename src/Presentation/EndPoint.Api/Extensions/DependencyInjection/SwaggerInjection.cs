using Microsoft.OpenApi.Models;

namespace EndPoint.Api.Extensions.DependencyInjection;

public static class SwaggerInjection
{
    public static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "shop", Version = "v1" });
        });

        return services;
    }
}