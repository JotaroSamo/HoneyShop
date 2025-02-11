using HoneyShop.Model.Models.Order;

namespace HoneyShop.Model.Models.User;

public class UserOrder
{
    public int Id { get; set; } // Идентификатор пользователя
    public string Username { get; set; } // Полное имя пользователя
    public ICollection<OrderItem> OrderItems { get; set; } // Список товаров в заказе
}