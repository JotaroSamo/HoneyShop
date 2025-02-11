using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Core.Contracts.Order;
using HoneyShop.Core.Excpetions;
using HoneyShop.Model.Models.Order;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Orders.ChangeStatusProduct;

public class ChangeStatusOrderCommandHandler : ICommandHandler<ChangeStatusOrderCommand,OrderItem>
{
    private readonly IOrderService _orderService;
    private readonly IHttpContextService _httpContextService;
    private readonly ILogger<ChangeStatusOrderCommandHandler> _logger;

    public ChangeStatusOrderCommandHandler(IOrderService orderService, IHttpContextService httpContextService,
        ILogger<ChangeStatusOrderCommandHandler> logger)
    {
        _orderService = orderService;
        _httpContextService = httpContextService;
        _logger = logger;
    }
    public async Task<OrderItem> Handle(ChangeStatusOrderCommand request, CancellationToken cancellationToken)
    {
        var updateUserId = _httpContextService.GetCurrentUserId();
        if (updateUserId == null)
        {
            _logger.LogError("Пользователь не установлен", 403);
            throw new HoneyException("Недостаточно прав", 403);
        }
        return await _orderService.ChangeStatus(request.OrderId, (int)request.Status, updateUserId.Value);
    
    }
}