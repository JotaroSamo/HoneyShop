namespace HonaeyShop.Domain.Model;

// Модель заказа
public class Order
{
    public int Id { get; set; } // Идентификатор заказа
    public int UserId { get; set; } // Идентификатор пользователя
    public User User { get; set; } // Связь с пользователем
    public decimal TotalPrice { get; set; } // Общая стоимость заказа
    
    public int StatusId { get; set; } // Идентификатор статуса заказа
    public OrderStatus Status { get; set; } // Статус заказа (например, Pending, Completed, Cancelled)
    public DateTime CreatedAt { get; set; } // Дата создания записи
    
    public string? Message { get; set; } // Сообщение о заказе
    
    public ICollection<History> Histories { get; set; } // Связь с историей изменений
    public ICollection<OrderDetail> OrderDetails { get; set; } // Связь с деталями заказа
}