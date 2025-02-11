using HonaeyShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoneyShop.DataAccess.Context.Configurate;

public class ApplicationFileConfiguration : IEntityTypeConfiguration<ApplicationFile>
{
    public void Configure(EntityTypeBuilder<ApplicationFile> builder)
    {
        builder.ToTable("ApplicationFiles"); // Имя таблицы в базе данных

        // Уникальный идентификатор
        builder.HasKey(a => a.Id);

        // Свойства
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(255); // Ограничение на длину имени файла

        builder.Property(a => a.Length)
            .IsRequired();

        builder.Property(a => a.ContentType)
            .IsRequired();

        builder.Property(a => a.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP"); // Значение по умолчанию для даты создания

        // Связь с пользователем
        builder.HasOne(a => a.CreatedBy)
            .WithMany() 
            .HasForeignKey(a => a.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict); // Поведение при удалении
        
        
    }
}
