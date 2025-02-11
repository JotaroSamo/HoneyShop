namespace HoneyShop.Model.Models.Product;

public class ProductItem
{
    public int Id { get; set; } // Идентификатор продукта
    public string Name { get; set; } // Название продукта
    public string Description { get; set; } // Описание продукта
    public decimal Price { get; set; } // Цена продукта
    public string Status { get; set; } // Статус продукта (например, Available, Out of Stock)
    
    public List<int> Files { get; set; }
}