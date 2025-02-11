using HoneyShop.Application.Orders.CreateOrder;
using HoneyShop.Model.Models.Order;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HoneyShop.Application.Orders.ChangeStatusProduct;

using HoneyShop.Application.Orders.DeleteOrder;
using HoneyShop.Application.Orders.GetOrders;
using HoneyShop.Application.Orders.GetOrdersByProduct;
using HoneyShop.Application.Orders.GetOrdersByUser;

using HoneyShop.Model.Enums;

using HoneyShop.Model.Pagination;


namespace HoneyShop.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "AMU")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
 
    }
    [Authorize(Policy = "AMU")]
    [HttpPost("create")]
    public async Task<ActionResult<OrderItem>> Create(CreateOrder createOrder)
    {
        var result = await _mediator.Send(new CreateOrderCommand(createOrder));
        return Ok(result);
    }
    [Authorize(Policy = "AMU")]
    [HttpGet("user")]
    public async Task<ActionResult<PaginationListModel<OrderItem>>> GetOrdersByUser([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
    {

        var result = await _mediator.Send(new GetOrdersByUserQuery( page, pageSize));
        return Ok(result);
    }
    [Authorize(Policy = "AM")]
    [HttpGet("product/{productId:int}")]
    public async Task<ActionResult<PaginationListModel<OrderItem>>> GetOrdersByProduct(int productId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetOrdersByProductQuery(productId, page, pageSize));
        return Ok(result);
    }
    [Authorize(Policy = "AM")]
    [HttpGet]
    public async Task<ActionResult<PaginationListModel<OrderItem>>> GetOrders([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetOrdersQuery(page, pageSize));
        return Ok(result);
    }
    [Authorize(Policy = "AM")]
    [HttpPatch("status/{orderid:int}")]
    public async Task<ActionResult<OrderItem>> ChangeStatus(int orderid, OrderStatus status)
    {
        var result = await _mediator.Send(new ChangeStatusOrderCommand(orderid, status));
        return Ok(result);
    }
    [Authorize(Policy = "AM")]
    [HttpDelete("{orderId:int}")]
    public async Task<ActionResult<bool>> DeleteOrder(int orderId)
    {
        var  result = _mediator.Send(new DeleteOrderCommand(orderId));
        return Ok(result);
    }

}