using HonaeyShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoneyShop.DataAccess.Context.Configurate;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(c => c.Id); // Установка первичного ключа
        builder.HasOne(c => c.User).WithMany(u => u.Carts).HasForeignKey(c => c.UserId); // Связь с пользователем через UserId
        builder.HasOne(c => c.Product).WithMany(p => p.Carts).HasForeignKey(c => c.ProductId); // Связь с продуктом через ProductId
        builder.Property(c => c.Quantity).IsRequired(); // Поле Quantity обязательно
        builder.Property(c => c.CreatedAt).IsRequired(); // Поле CreatedAt обязательно
    }
}