using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HoneyShop.DataAccess.Context;

public class HoneyShopDbContextFactory : IDesignTimeDbContextFactory<HoneyShopDbContext>
{
    public HoneyShopDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HoneyShopDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Honey;Username=postgres;Password=postgres");

        return new HoneyShopDbContext(optionsBuilder.Options);
    }
}
