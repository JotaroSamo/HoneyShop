using HoneyShop.Application.Core.Commands.Contracts;

namespace HoneyShop.Application.Carts.DeleteCart;

public class DeleteCartCommand : ICommand<bool>
{
    public int Id { get; }

    public DeleteCartCommand(int id)
    {
        Id = id;
    }
}