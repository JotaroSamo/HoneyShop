using HonaeyShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoneyShop.DataAccess.Context.Configurate;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id); // Установка первичного ключа
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100); // Поле Name обязательно и имеет максимальную длину 100
        builder.Property(p => p.Description).HasMaxLength(500); // Поле Description имеет максимальную длину 500
        builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)"); // Поле Price обязательно и имеет тип decimal
        builder.Property(p => p.CreatedAt).IsRequired(); // Поле CreatedAt обязательно
        builder.Property(p => p.UpdatedAt).IsRequired(); // Поле UpdatedAt обязательно
        builder.Property(p => p.IsRemoved).IsRequired();
        builder.HasOne(o => o.Status).WithMany(o => o.Products).HasForeignKey(o => o.StatusId);
        builder.HasMany(p => p.Files)
            .WithOne(f => f.Product) // Здесь нужно указать обратную связь
            .HasForeignKey(f => f.ProductId) // Предполагается, что в ApplicationFile есть поле ProductId
            .OnDelete(DeleteBehavior.Restrict); // Поведение при удалении

        builder.HasMany(p => p.Histories).WithOne().OnDelete(DeleteBehavior.Cascade);

        // Добавление дефолтного продукта
        builder.HasData(new Product
        {
            Id = 1,
            Name = "Default Product",
            Description = "This is a default product",
            Price = 0.00m,
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), // Указание UTC
            UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            StatusId = 1 // Предполагается, что статус с Id = 1 существует
        });
    }

}