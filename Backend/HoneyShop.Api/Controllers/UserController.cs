using HoneyShop.Application.Users.ChangePassword;
using HoneyShop.Application.Users.ChangePasswordAdmin;
using HoneyShop.Application.Users.ChangeRoleAdmin;
using HoneyShop.Application.Users.CreateUser;
using HoneyShop.Application.Users.CreateUserAdminRoot;
using HoneyShop.Application.Users.DeleteUser;
using HoneyShop.Application.Users.DeleteUserAdmin;
using HoneyShop.Application.Users.GetUsersAdmin;
using HoneyShop.Application.Users.UpdateUser;
using HoneyShop.Application.Users.UpdateUserAdmin;
using HoneyShop.Model.Enums;
using HoneyShop.Model.Models.User;
using HoneyShop.Model.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShop.Controllers;

[ApiController]
[Authorize(Policy = "AMU")]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
   
    
    [HttpPost("create-admin")]
    [Authorize(Policy = "A")]
    public async Task<ActionResult<UserItem>> CreateUserForAdmin(CreateUserAdminRoot user)
    {
        return await _mediator.Send(new CreateUserAdminRootCommand(user));
    }
    [HttpPatch("update-admin")]
    [Authorize(Policy = "A")]
    public async Task<ActionResult<UserItem>> UpdateUserForAdmin(UpdateUser updateUser)
    {
        return await _mediator.Send(new UpdateUserAdminCommand(updateUser));
    }
    
    [HttpPatch("update-role-admin")]
    [Authorize(Policy = "A")]
    public async Task<ActionResult<UserItem>> UpdateUserForAdmin(int id, Role role)
    {
        return await _mediator.Send(new ChangeRoleAdminCommand(id, role));
    }
    
    [HttpPatch("change-password-admin")]
    [Authorize(Policy = "A")]
    public async Task<ActionResult<UserItem>> ChangePasswordForAdmin(UpdatePasswordUser updatePasswordUser)
    {
        return await _mediator.Send(new ChangePasswordAdminCommand(updatePasswordUser));
    }
    
    [HttpDelete("delete-user-admin")]
    [Authorize(Policy = "A")]
    public async Task<ActionResult<bool>> DeleteUserForAdmin(int id)
    {
        return await _mediator.Send(new DeleteUserAdminCommand(id));
    }
    
    [HttpGet("get-users-admin")]
    [Authorize(Policy = "A")]
    public async Task<ActionResult<PaginationListModel<UserItem>>> GetUsers(int page, int pageSize)
    {
        return await _mediator.Send(new GetUsersAdminQuery(page, pageSize));
    }

    [HttpPost("create")]
    [AllowAnonymous]
    public async Task<ActionResult<UserItem>> CreateUserForUser(CreateUser user)
    {
        return await _mediator.Send(new CreateUserCommand(user));
    }
    
    [HttpPost("update")]
    [Authorize(Policy = "AMU")]
    public async Task<ActionResult<UserItem>> UpdateUserForUser(UpdateUser updateUser)
    {
        return await _mediator.Send(new UpdateUserAdminCommand(updateUser));
    }
    [HttpPatch("change-password")]
    [Authorize(Policy = "AMU")]
    public async Task<ActionResult<UserItem>> ChangePasswordForUser(UpdatePasswordUser updatePasswordUser)
    {
        return await _mediator.Send(new ChangePasswordCommand(updatePasswordUser));
    }
    
    [HttpDelete("delete-user")]
    [Authorize(Policy = "A")]
    public async Task<ActionResult<bool>> DeleteUserForUser(int id)
    {
        return await _mediator.Send(new DeleteUserCommand(id));
    }
}