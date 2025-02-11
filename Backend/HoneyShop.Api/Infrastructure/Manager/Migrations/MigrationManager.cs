using Microsoft.EntityFrameworkCore;

namespace HoneyShop.Infrastructure.Manager.Migrations;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase<TContext>(this WebApplication host)
        where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<TContext>();

        var logger = services.GetRequiredService<ILogger<TContext>>();
        var contextName = typeof(TContext).Name;

        try
        {
            logger.LogInformation("Migrating database associated with context {ContextName}", contextName);
            context.Database.Migrate();
            logger.LogInformation("Database associated with context {ContextName} has been migrated", contextName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured while migrating the database used on context {ContextName}, {se}", contextName, services);
            Console.WriteLine(ex);
        }
        finally
        {
            context.Dispose();
        }

        return host;
    }
}