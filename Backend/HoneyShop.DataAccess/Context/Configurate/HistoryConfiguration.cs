using HonaeyShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoneyShop.DataAccess.Context.Configurate;

public class HistoryConfiguration: IEntityTypeConfiguration<History>
{
    public void Configure(EntityTypeBuilder<History> builder)
    {
        builder.HasKey(o => o.Id); // Установка первичного ключа
    }
}