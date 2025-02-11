namespace HonaeyShop.Domain.Model;

// Модель детали заказа
public class OrderDetail
{
    public int Id { get; set; } // Идентификатор детали заказа
    public int OrderId { get; set; } // Идентификатор заказа
    public Order Order { get; set; } // Связь с заказом
    public int ProductId { get; set; } // Идентификатор продукта
    public Product Product { get; set; } // Связь с продуктом
    public int Quantity { get; set; } // Количество продукта
}