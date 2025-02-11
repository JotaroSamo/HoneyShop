using HoneyShop.Core.Constant;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace HoneyShop.Infrastructure.Configurations;

public static class HealthCheckConfiguration
{
    public static void AddApplicationHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddCheck("Application API",
                _ => HealthCheckResult.Healthy(),
                tags: new[] { "app_api", "api" })
            .AddMongoDb(
                clientFactory: sp => new MongoClient(configuration.GetConnectionString(ConnectionStringNames.MongoDb)),
                databaseNameFactory: sp => "logsDatabase", 
                name: "Logs Storage",
                tags: new[] { "mongodb", "logs" })
            .AddNpgSql(configuration.GetConnectionString(ConnectionStringNames.Honey),
                tags: new[] { "postgresql", "data" },
                name: $"Data Storage");
    }
}