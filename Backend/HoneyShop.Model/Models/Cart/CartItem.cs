namespace HoneyShop.Model.Models.Cart;

public class CartItem
{
    public int Id { get; set; }
    public int ProductId { get; set; } // Идентификатор продукта
    public string ProductName { get; set; } // Название продукта
    public List<int> FileIds { get; set; } // Идентификаторы файлов
    public decimal Price { get; set; } // Цена продукта
    public int Quantity { get; set; } // Количество товара в корзине
}