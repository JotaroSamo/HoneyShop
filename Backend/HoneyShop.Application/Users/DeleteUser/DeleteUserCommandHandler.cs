using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Application.Users.DeleteUserAdmin;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Core.Contracts.User;
using HoneyShop.Core.Excpetions;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Users.DeleteUser;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserAdminCommand, bool>
{
    private readonly IUserService _userService;
    private readonly ILogger<DeleteUserAdminCommandHandler> _logger;
    private readonly IHttpContextService _httpContextService;

    public DeleteUserCommandHandler(IUserService userService, ILogger<DeleteUserAdminCommandHandler> logger, IHttpContextService httpContextService)
    {
        _userService = userService;
        _logger = logger;
        _httpContextService = httpContextService;
    }

    public async Task<bool> Handle(DeleteUserAdminCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начало удаления пользователя с ID {UserId}", request.Id);
        var user = await _userService.GetByIdForUpdatePassword(request.Id);
        var updateUserId = _httpContextService.GetCurrentUserId();
        if (user.Id != updateUserId)
        {
            _logger.LogError("Недостаточно прав для удаления пользователя с ID {UserId}", request.Id);
            throw new HoneyException("Недостаточно прав", 403);
        }
        var result = await _userService.DeleteUser(request.Id, updateUserId.Value);
        if (result)
        {
            _logger.LogInformation("Пользователь с ID {UserId} успешно удален", request.Id);
        }
        else
        {
            _logger.LogWarning("Не удалось удалить пользователя с ID {UserId}", request.Id);
        }
        return result;
    }
}