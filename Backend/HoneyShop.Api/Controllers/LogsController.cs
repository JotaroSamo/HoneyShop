using HoneyShop.Application.Logs.GetLogs;
using HoneyShop.DataAccess.MongoDb;
using HoneyShop.DataAccess.MongoDb.Models;
using HoneyShop.Model.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HoneyShop.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "A")]
public class LogsController : ControllerBase
{
    private readonly IMediator _mediator;


    public LogsController( IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = "A")]
    public async Task<ActionResult<PaginationListModel<LogModel>>> GetLogs([FromQuery] string? search,[FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetLogsQuery(search, page, pageSize));
        return Ok(result);
    }
}