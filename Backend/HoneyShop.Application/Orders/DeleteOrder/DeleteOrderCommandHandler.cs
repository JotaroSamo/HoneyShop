using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Core.Contracts.Order;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Orders.DeleteOrder;

public class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand, bool>
{
    private readonly IOrderService _orderService;
    private readonly ILogger<DeleteOrderCommandHandler> _logger;

    public DeleteOrderCommandHandler(IOrderService orderService, 
        ILogger<DeleteOrderCommandHandler> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }
    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        return await _orderService.DeleteOrder(request.OrderId);
    }
}