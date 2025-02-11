using HoneyShop.Model.Settings;

using System.Text;
using System.Text.Json.Serialization;
using HealthChecks.UI.Client;
using HoneyShop.Application.Behaviors;
using HoneyShop.DataAccess.Context;
using HoneyShop.Infrastructure.Configurations;
using HoneyShop.Infrastructure.Filters;
using HoneyShop.Infrastructure.Manager;
using HoneyShop.Infrastructure.Manager.Migrations;
using HoneyShop.Model.Settings.Cors;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.MongoDB(context.Configuration["Serilog:WriteTo:1:Args:databaseUrl"], collectionName: "logs"));
// Настройка сервисов
ConfigureServices(builder.Services, builder.Configuration, builder.Environment);

// Создание приложения
var app = builder.Build();

// Настройка middleware
ConfigureMiddleware(app);

app.Run();

// Метод для настройки сервисов
void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
{
    // Добавление конфигурации AppSettings в DI контейнер
    services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
    services.AddMemoryCache();
    services.AddApplicationHealthChecks(configuration);
    var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
    services.AddAuth(configuration, appSettings);
    services.AddMediatR(typeof(LoggingBehavior<,>).Assembly);
    // Настройка CORS
    ConfigureCors(services, configuration);
    services.AddDataContext(configuration, environment);
    services.AddDependencyInjection();
    // Добавление других сервисов
    services
        .AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()))
        .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    services.AddEndpointsApiExplorer();
    services.AddHttpClient();
    services.AddHttpContextAccessor();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

}
 

// Метод для настройки CORS
void ConfigureCors(IServiceCollection services, IConfiguration configuration)
{
    var corsSettings = configuration.GetSection("AppSettings:CorsSettings").Get<CorsSettings>();
   
    services.AddCors(options =>
    {
        options.AddPolicy("CustomCorsPolicy", policy =>
        {
            policy.WithOrigins(corsSettings.AllowedOrigins.ToArray())  // Разрешенные источники
                .AllowAnyMethod() // Разрешить все HTTP методы
                .AllowAnyHeader() // Разрешить все заголовки
                .AllowCredentials(); // Разрешить отправку учетных данных
        });
    });
}

void ConfigureMiddleware(WebApplication app)
{
    app.MigrateDatabase<HoneyShopDbContext>();
    
    app.UseDenyFrameMiddleware()
        .UseNoShiftContentMiddleware()
        .UseXssProtectionMiddleware();
    

    // Настройка Swagger для разработки
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "/openapi/{documentName}.json";
        });
        app.MapScalarApiReference();
    }

    // Применение CORS политики
    app.UseCors("CustomCorsPolicy");
    
    // Использование HTTPS редиректа
    app.UseHttpsRedirection();
    
    // Использование аутентификации и авторизации
    app.UseAuthentication();
    app.UseAuthorization();

   
    
    app.MapHealthChecks("/hc", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        
    });
    // Маршрутизация контроллеров
    app.MapControllers();
}
