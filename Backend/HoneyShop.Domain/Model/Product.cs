namespace HonaeyShop.Domain.Model;

// Модель продукта
public class Product
{
    public int Id { get; set; } // Идентификатор продукта
    public string Name { get; set; } = string.Empty;// Название продукта
    public string Description { get; set; } = string.Empty;// Описание продукта
    public decimal Price { get; set; } // Цена продукта
    
    public int StatusId { get; set; } // Идентификатор статуса продукта
    public ProductStatus  Status{ get; set; }  // Статус продукта 
    public DateTime CreatedAt { get; set; } // Дата создания записи
    public DateTime UpdatedAt { get; set; } // Дата обновления записи
    
    public bool IsRemoved { get; set; }
    public ICollection<Cart> Carts { get; set; } // Связь с корзинами
    
    public ICollection<OrderDetail> OrderDetails { get; set; } // Связь с деталями заказов
    
    public ICollection<ApplicationFile> Files { get; set; } // Связь с файлами
    
    public ICollection<History> Histories { get; set; } // Связь с историей изменений
}