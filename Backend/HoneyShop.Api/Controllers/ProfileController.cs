using HoneyShop.Application.Profile.GetProfile;
using HoneyShop.Model.Models.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShop.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<UserItem>> Me()
    {
        var user = await _mediator.Send(new GetProfileQuery());
        return Ok(user);
    }
}