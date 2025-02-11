using HonaeyShop.Domain.Model;
using HoneyShop.DataAccess.Context.Configurate;
using Microsoft.EntityFrameworkCore;

namespace HoneyShop.DataAccess.Context;

public class HoneyShopDbContext : DbContext
{
    public DbSet<User> Users { get; set; } // Таблица пользователей
    public DbSet<Role> Roles { get; set; } // Таблица ролей
    public DbSet<Product> Products { get; set; } // Таблица продуктов
    public DbSet<Order> Orders { get; set; } // Таблица заказов
    public DbSet<OrderDetail> OrderDetails { get; set; } // Таблица деталей заказов
    public DbSet<Cart> Carts { get; set; } // Таблица корзин
    public DbSet<ApplicationFile> Files { get; set; } //Таблица с файлами

    public HoneyShopDbContext(DbContextOptions<HoneyShopDbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly); // Применение конфигурации для User
    }
}