using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Core.Contracts.User;
using HoneyShop.Core.Excpetions;
using HoneyShop.Model.Models.User;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Users.UpdateUserAdmin;

public class UpdateUserAdminCommandHandler : ICommandHandler<UpdateUserAdminCommand, UserItem>
{
    private readonly IUserService _userService;
    private readonly ILogger<UpdateUserAdminCommandHandler> _logger;
    private readonly IHttpContextService _httpContextService;

    public UpdateUserAdminCommandHandler(IUserService userService, ILogger<UpdateUserAdminCommandHandler> logger, IHttpContextService httpContextService)
    {
        _userService = userService;
        _logger = logger;
        _httpContextService = httpContextService;
    }

    public async Task<UserItem> Handle(UpdateUserAdminCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начало обновления пользователя с ID {UserId}", request.UpdateUser.Id);
        var updateUserId = _httpContextService.GetCurrentUserId();
        if ( updateUserId==null)
        {
            _logger.LogError("Недостаточно прав для обновления пользователя с ID {UserId}", request.UpdateUser.Id);
            throw new HoneyException("Недостаточно прав", 403);
        }
        var updatedUser = await _userService.Update(request.UpdateUser, updateUserId.Value);
        _logger.LogInformation("Пользователь с ID {UserId} успешно обновлен", request.UpdateUser.Id);
        return updatedUser;
    }
}
