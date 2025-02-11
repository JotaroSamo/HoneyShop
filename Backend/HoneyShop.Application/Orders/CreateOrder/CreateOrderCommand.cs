using System.Windows.Input;
using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Model.Models.Cart;
using HoneyShop.Model.Models.Order;

namespace HoneyShop.Application.Orders.CreateOrder;

public class CreateOrderCommand :ICommand<OrderItem>
{
    public Model.Models.Order.CreateOrder CreateCart { get; }

    public CreateOrderCommand(Model.Models.Order.CreateOrder createCart)
    {
        CreateCart = createCart;
    }
}