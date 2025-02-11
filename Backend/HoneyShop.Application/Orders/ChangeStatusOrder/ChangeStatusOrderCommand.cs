using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Model.Enums;
using HoneyShop.Model.Models.Order;

namespace HoneyShop.Application.Orders.ChangeStatusProduct;

public class ChangeStatusOrderCommand : ICommand<OrderItem>
{
    public int OrderId { get; }
    public OrderStatus Status { get; }

    public ChangeStatusOrderCommand(int orderId, OrderStatus status)
    {
        OrderId = orderId;
        Status = status;
    }
}