using HonaeyShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoneyShop.DataAccess.Context.Configurate;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id); // Установка первичного ключа
        builder.Property(r => r.Name).IsRequired().HasMaxLength(20); // Поле Name обязательно и имеет максимальную длину 20
        
        builder.HasData(
            new Role { Id = 1, Name = "Админ" },
            new Role { Id = 2, Name = "Менеджер" },
            new Role { Id = 3, Name = "Пользователь" }
        );
    }
}