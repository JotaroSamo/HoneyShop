using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Application.Users.ChangePasswordAdmin;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Core.Contracts.Security;
using HoneyShop.Core.Contracts.User;
using HoneyShop.Core.Excpetions;
using HoneyShop.Model.Models.User;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Users.ChangePassword;

public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordAdminCommand, UserItem>
{
    private readonly IUserService _userService;
    private readonly IPasswordService _passwordService;
    private readonly ILogger<ChangePasswordAdminCommandHandler> _logger;
    private readonly IHttpContextService _httpContextService;

    public ChangePasswordCommandHandler(IUserService userService, IPasswordService passwordService,
        ILogger<ChangePasswordAdminCommandHandler> logger, IHttpContextService httpContextService)
    {
        _userService = userService;
        _passwordService = passwordService;
        _logger = logger;
        _httpContextService = httpContextService;
    }

    public async Task<UserItem> Handle(ChangePasswordAdminCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начало изменения пароля для пользователя с ID {UserId}", request.PasswordUser.Id);
        var user = await _userService.GetByIdForUpdatePassword(request.PasswordUser.Id);
        var updateUserId = _httpContextService.GetCurrentUserId();
        if (user.Id != updateUserId)
        {
            _logger.LogError("Недостаточно прав для изменения пароля пользователя с ID {UserId}",
                request.PasswordUser.Id);
            throw new HoneyException("Недостаточно прав", 403);
        }

        if (!_passwordService.VerifyPassword(user.PasswordHash, request.PasswordUser.Password))
        {
            _logger.LogError("Неверный текущий пароль для пользователя с ID {UserId}", request.PasswordUser.Id);
            throw new HoneyException("Неверный пароль", 400);
        }

        var hashPassword = _passwordService.HashPassword(request.PasswordUser.NewPassword);
        var updatedUser = await _userService.ChangePassword(request.PasswordUser.Id, hashPassword, updateUserId.Value);
        _logger.LogInformation("Пароль успешно изменен для пользователя с ID {UserId}", request.PasswordUser.Id);
        return updatedUser;
    }
}