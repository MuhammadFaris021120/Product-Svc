using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Common.Behaviors;

namespace Product.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        // Finds every AbstractValidator<T> in the Application project
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
