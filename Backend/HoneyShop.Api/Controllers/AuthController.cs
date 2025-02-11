using HoneyShop.Application.Users.LoginUser;
using HoneyShop.Model.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShop.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<ActionResult<JwtModel>> Login(LoginModel model)
    {
        var jwt = await _mediator.Send(new LoginUserCommand(model.UserName, model.Password));
        return Ok(jwt);
    }
}