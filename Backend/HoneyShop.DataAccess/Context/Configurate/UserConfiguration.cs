using HonaeyShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoneyShop.DataAccess.Context.Configurate;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id); // Установка первичного ключа
        builder.Property(u => u.Username).IsRequired().HasMaxLength(50); // Поле Username обязательно и имеет максимальную длину 50
        builder.HasIndex(u => u.Username).IsUnique(); // Уникальный индекс на поле Username
        builder.Property(u => u.PasswordHash).IsRequired(); // Поле PasswordHash обязательно
        builder.Property(u => u.FullName).HasMaxLength(100).IsRequired();; // Поле FullName обязательно и имеет максимальную длину 100
        builder.Property(u => u.PhoneNumber).HasMaxLength(15).IsRequired();; // Поле PhoneNumber обязательно и имеет максимальную длину 15
        builder.HasIndex(u => u.TelegramId).IsUnique(); // Создание некластеризованного индекса на поле TelegramLongId
        builder.HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId); // Связь с ролью через RoleId
        builder.Property(u => u.IsRemoved).IsRequired();
        builder.Property(u => u.CreatedAt).IsRequired(); // Поле CreatedAt обязательно
        builder.Property(u => u.UpdatedAt).IsRequired(); // Поле UpdatedAt обязательно
        builder.HasMany(p => p.Histories).WithOne().OnDelete(DeleteBehavior.Cascade);
    }
}