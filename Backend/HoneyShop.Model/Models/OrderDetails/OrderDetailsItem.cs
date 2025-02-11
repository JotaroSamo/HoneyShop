using HoneyShop.Model.Models.Product;

namespace HoneyShop.Model.Models.OrderDetails;

public class OrderDetailsItem
{
    public int Id { get; set; } // Идентификатор детали заказа
    public ProductItem Product { get; set; } // Связь с продуктом
    public int Quantity { get; set; } // Количество продукта
}