using HoneyShop.Application;
using HoneyShop.BusinessLogic;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Infrastructure.Context;

namespace HoneyShop.Infrastructure.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IHttpContextService, HttpContextService>();
        services.AddBusinessLogicDependencies()
            .AddApplicationPipeline();
    }
}