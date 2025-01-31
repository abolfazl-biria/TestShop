using Application.Validators.Products;
using FluentValidation;

namespace EndPoint.Api.Extensions.DependencyInjection;

public static class ValidationInjection
{
    public static IServiceCollection AddConfiguredValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(AddProductCommandValidator).Assembly);

        return services;
    }
}