using HoneyShop.Application.Core.Commands.Contracts;

namespace HoneyShop.Application.Products.DeleteProduct;

public class DeleteProductCommand : ICommand<bool>
{
    public int Id { get; }

    public DeleteProductCommand(int id)
    {
        Id = id;
    }
}