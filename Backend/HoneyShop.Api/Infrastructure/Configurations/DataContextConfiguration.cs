using HoneyShop.Core.Constant;
using HoneyShop.DataAccess.Context;
using HoneyShop.DataAccess.MongoDb;
using HoneyShop.Model.Settings;
using Microsoft.EntityFrameworkCore;

namespace HoneyShop.Infrastructure.Configurations;
public static class DataContextConfiguration
{
    public static void AddDataContext(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
    
   

        services.AddDbContext<HoneyShopDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(ConnectionStringNames.Honey), npgsqlOptions =>
                npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)
                    .CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds));

            if (webHostEnvironment.IsDevelopment())
            {
                // Включаем подробные логи и ошибку в режиме разработки
                options.EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }
        });
        
        services.Configure<MongoContextService>(configuration);
        services.AddSingleton<MongoContextService>();
    }
}
