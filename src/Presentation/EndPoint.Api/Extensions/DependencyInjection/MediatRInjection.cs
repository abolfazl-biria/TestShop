using Application.Behaviors;
using Application.Models.Commands.Products;
using MediatR;

namespace EndPoint.Api.Extensions.DependencyInjection;

public static class MediatRInjection
{
    public static IServiceCollection AddConfiguredMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {

            cfg.RegisterServicesFromAssemblies(typeof(AddProductCommand).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        return services;
    }
}