using HoneyShop.Model.Models.OrderDetails;

namespace HoneyShop.Model.Models.Order;

public class OrderItem
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; } // Цена продукта
    public int Quantity { get; set; } // Количество товара в корзине
    public string FullName { get; set; } // Имя пользователя
    public string Number { get; set; } // Номер телефона
    public string Status { get; set; } // Статус заказа (например, Pending, Completed, Cancelled)
    public ICollection<OrderDetailsItem> OrderDetails { get; set; } // Связь с деталями заказа
}