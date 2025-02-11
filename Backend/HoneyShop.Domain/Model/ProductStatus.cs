namespace HonaeyShop.Domain.Model;

public class ProductStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Product> Products { get; set; }
}