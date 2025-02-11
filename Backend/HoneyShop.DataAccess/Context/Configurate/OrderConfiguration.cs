using HonaeyShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoneyShop.DataAccess.Context.Configurate;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id); // Установка первичного ключа
        builder.HasOne(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId); // Связь с пользователем через UserId
        builder.Property(o => o.TotalPrice).IsRequired().HasColumnType("decimal(18,2)"); // Поле TotalPrice обязательно и имеет тип decimal
        builder.HasOne(o=>o.Status).WithMany(o=>o.Orders).HasForeignKey(o=>o.StatusId); // Связь с статусом заказа через StatusId
        builder.HasMany(p => p.Histories).WithOne().OnDelete(DeleteBehavior.Cascade);
        builder.Property(o => o.CreatedAt).IsRequired(); // Поле CreatedAt обязательно
    }
}