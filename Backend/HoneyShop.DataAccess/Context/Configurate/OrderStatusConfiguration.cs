using HonaeyShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoneyShop.DataAccess.Context.Configurate;

public class OrderStatusConfiguration: IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.ToTable("OrderStatuses");
        builder.HasKey(r => r.Id); // Установка первичного ключа
        builder.Property(r => r.Name).IsRequired().HasMaxLength(20); // Поле Name обязательно и имеет максимальную длину 20
        
        builder.HasData(
            new OrderStatus { Id = 1, Name = "На проверке" },
            new OrderStatus { Id = 2, Name = "Одобрен" },
            new OrderStatus { Id = 3, Name = "Отклонен" }
        );
    }
}