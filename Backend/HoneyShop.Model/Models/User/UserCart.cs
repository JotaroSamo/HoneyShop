using HoneyShop.Model.Models.Cart;

namespace HoneyShop.Model.Models.User;

public class UserCart
{
    public int UserId { get; set; } // Идентификатор пользователя
    public ICollection<CartItem> CartItems { get; set; } // Список товаров в корзине
}