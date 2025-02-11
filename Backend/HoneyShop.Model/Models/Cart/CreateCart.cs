using System.ComponentModel.DataAnnotations;

namespace HoneyShop.Model.Models.Cart;

public class CreateCart
{

    [Required]
    public int ProductId { get; set; } // Идентификатор продукта
    [Required]
    public int Quantity { get; set; } // Количество продукта

}