using HonaeyShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoneyShop.DataAccess.Context.Configurate;

public class ProductStatusConfigurate : IEntityTypeConfiguration<ProductStatus>
{
    public void Configure(EntityTypeBuilder<ProductStatus> builder)
    {
        builder.ToTable("ProductStatuses");
        builder.HasKey(r => r.Id); // Установка первичного ключа
        builder.Property(r => r.Name).IsRequired().HasMaxLength(20); // Поле Name обязательно и имеет максимальную длину 20
 
        builder.HasData(
            new OrderStatus { Id = 1, Name = "В наличии" },
            new OrderStatus { Id = 2, Name = "Нет в наличии" },
            new OrderStatus { Id = 3, Name = "Нет данных" }
        );
    }
}