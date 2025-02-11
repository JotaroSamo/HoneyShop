using HoneyShop.Model.Enums;
using HoneyShop.Model.Models.OrderDetails;

namespace HoneyShop.Model.Models.Order;

public class CreateOrder
{
    public OrderStatus Status { get; set; } // Статус заказа (например, Pending, Completed, Cancelled)
    public string Message { get; set; } // Сообщение о заказе
    public ICollection<CreateOrderDetails> OrderDetails { get; set; } // Связь с деталями заказа
}