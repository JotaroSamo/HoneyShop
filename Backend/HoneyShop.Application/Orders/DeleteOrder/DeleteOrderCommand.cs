using HoneyShop.Application.Core.Commands.Contracts;

namespace HoneyShop.Application.Orders.DeleteOrder;

public class DeleteOrderCommand : ICommand<bool>
{
    public int OrderId { get; }

    public DeleteOrderCommand(int orderId)
    {
        OrderId = orderId;
    }
}