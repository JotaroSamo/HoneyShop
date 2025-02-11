using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Model.Models.Cart;

namespace HoneyShop.Application.Carts.CreateCart;

public class CreateCartCommand : ICommand<CartItem>
{
    public Model.Models.Cart.CreateCart Cart { get; }

    public CreateCartCommand(Model.Models.Cart.CreateCart cart)
    {
        Cart = cart;
    }
}