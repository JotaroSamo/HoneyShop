using HoneyShop.Application.Products.ChangeStatusProduct;
using HoneyShop.Application.Products.CreateProduct;
using HoneyShop.Application.Products.DeleteProduct;
using HoneyShop.Application.Products.GetProductById;
using HoneyShop.Application.Products.GetProductsPage;
using HoneyShop.Application.Products.UpdateProduct;
using HoneyShop.Model.Enums;
using HoneyShop.Model.Models.Product;
using HoneyShop.Model.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShop.Controllers;
[ApiController]

[Route("api/[controller]")]
[Authorize(Policy = "AMU")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [Route("create")]
    [Authorize(Policy = "AM")]
    public async Task<ActionResult<ProductItem>> Create(CreateProduct product)
    {
        var result = await _mediator.Send(new CreateProductCommand(product));
        return Ok(result);
    }
    
    [HttpGet]
    [Route("get-page")]
    [AllowAnonymous]
    public async Task<ActionResult<PaginationListModel<ProductItem>>> GetPage([FromQuery]int page, [FromQuery]int pageSize)
    {
        var result = await _mediator.Send(new GetProductsPageQuery(page, pageSize));
        return Ok(result);
    }

    [HttpPatch]
    [Route("change-status")]
    [Authorize(Policy = "AM")]
    public async Task<ActionResult<ProductItem>> ChangeStatus(int id, ProductStatusEnum status)
    {
        var result = await _mediator.Send(new ChangeStatusProductCommand(id, status));
        return Ok(result);
    
    }
    [HttpPut]
    [Route("update")]
    [Authorize(Policy = "AM")]
    public async Task<ActionResult<ProductItem>> Update(UpdateProduct product)
    {
        var result = await _mediator.Send(new UpdateProductCommand(product));
        return Ok(result);
    
    }
    [HttpGet]
    [Route("get-by-id/{id}")]
    [Authorize(Policy = "AMU")]
    public async Task<ActionResult<ProductItem>> GetById(int id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(result);
    
    }
    [HttpDelete]
    [Route("delete/{id}")]
    [Authorize(Policy = "AM")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));
        return Ok(result);
    
    }
}