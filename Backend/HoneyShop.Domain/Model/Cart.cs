namespace HonaeyShop.Domain.Model;

// Модель корзины
public class Cart
{
    public int Id { get; set; } // Идентификатор корзины
    public int UserId { get; set; } // Идентификатор пользователя
    public User User { get; set; } // Связь с пользователем
    public int ProductId { get; set; } // Идентификатор продукта
    public Product Product { get; set; } // Связь с продуктом
    public int Quantity { get; set; } // Количество продукта
    public DateTime CreatedAt { get; set; } // Дата создания записи
}