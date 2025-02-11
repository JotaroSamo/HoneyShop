using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Core.Contracts.User;
using HoneyShop.Core.Excpetions;
using HoneyShop.Model.Models.User;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Users.ChangeRoleAdmin;

public class ChangeRoleAdminCommandHandler : ICommandHandler<ChangeRoleAdminCommand, UserItem>
{
    private readonly IUserService _userService;
    private readonly ILogger<ChangeRoleAdminCommandHandler> _logger;
    private readonly IHttpContextService _httpContextService;

    public ChangeRoleAdminCommandHandler(IUserService userService, ILogger<ChangeRoleAdminCommandHandler> logger, IHttpContextService httpContextService)
    {
        _userService = userService;
        _logger = logger;
        _httpContextService = httpContextService;
    }

    public async Task<UserItem> Handle(ChangeRoleAdminCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начало изменения роли для пользователя с ID {UserId}", request.Id);
        var updateUserId = _httpContextService.GetCurrentUserId();
        if (updateUserId == null)
        {
            _logger.LogError("Недостаточно прав изменения роли пользователя с ID {UserId}", request.Id);
            throw new HoneyException("Недостаточно прав", 403);
        }
        var updatedUser = await _userService.ChangeRoleAdmin(request.Id, (int)request.Role, updateUserId.Value);
        _logger.LogInformation("Роль успешно изменена для пользователя с ID {UserId}", request.Id);
        return updatedUser;
    }
}