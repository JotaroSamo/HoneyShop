
using HoneyShop.Application.Carts.CreateCart;
using HoneyShop.Application.Carts.DeleteCart;
using HoneyShop.Application.Carts.GetCartItemsPage;
using HoneyShop.Model.Models.Cart;
using HoneyShop.Model.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShop.Controllers;
[ApiController]

[Route("api/[controller]")]
[Authorize(Policy = "AMU")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [Route("create")]
    [Authorize(Policy = "AMU")]
    public async Task<ActionResult<CartItem>> Create(CreateCart cart)
    {
        var result = await _mediator.Send(new CreateCartCommand(cart));
        return Ok(result);
    }
    [HttpGet]
    [Route("get-page")]
    [Authorize(Policy = "AMU")]
    public async Task<ActionResult<PaginationListModel<CartItem>>> GetPage([FromQuery]int page, [FromQuery]int pageSize)
    {
        var result = await _mediator.Send(new GetCartItemsPageQuery(page, pageSize));
        return Ok(result);
    }
    [HttpDelete]
    [Route("delete")]
    [Authorize(Policy = "AMU")]
    public async Task<ActionResult<bool>> CreateForUser(int id)
    {
        var result = await _mediator.Send(new DeleteCartCommand(id));
        return Ok(result);
    }
}