using HonaeyShop.Domain.Model;
using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Core.Contracts.Order;
using HoneyShop.Core.Excpetions;
using HoneyShop.Model.Models.Order;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Orders.CreateOrder;

public class CreateOrderCommandHandler :ICommandHandler<CreateOrderCommand, OrderItem>
{
    private readonly IOrderService _orderService;
    private readonly IHttpContextService _httpContextService;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IOrderService orderService, IHttpContextService httpContextService,
        ILogger<CreateOrderCommandHandler> logger)
    {
        _orderService = orderService;
        _httpContextService = httpContextService;
        _logger = logger;
    }

    public async Task<OrderItem> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var createUserId = _httpContextService.GetCurrentUserId();
        if (createUserId == null)
        {
            _logger.LogError("Пользователь не установлен", 403);
            throw new HoneyException("Недостаточно прав", 403);
        }

        var orderDetails = new List<OrderDetail>();

        foreach (var detail in request.CreateCart.OrderDetails)
        {
            var orderDetail = new OrderDetail
            {
                ProductId = detail.ProductId,
                Quantity = detail.Quantity
            };
            orderDetails.Add(orderDetail);
        }

        var order = new Order
        {
            StatusId = (int)request.CreateCart.Status,
            Message = request.CreateCart.Message,
            OrderDetails = orderDetails
        };

        return await _orderService.CreateOrder(order, createUserId.Value);
    }

}