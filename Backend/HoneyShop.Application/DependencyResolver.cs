using HoneyShop.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HoneyShop.Application;

public static class DependencyResolver
{
    public static IServiceCollection AddApplicationPipeline(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return services;
    }
}