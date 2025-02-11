using HonaeyShop.Domain.Model;
using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Application.Users.CreateUser;
using HoneyShop.Core.Contracts.Security;
using HoneyShop.Core.Contracts.User;
using HoneyShop.Model.Models.User;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Users.CreateUserAdminRoot;

public class CreateUserAdminRootCommandHandler : ICommandHandler<CreateUserAdminRootCommand, UserItem>
{
    private readonly IUserService _userService;
    private readonly IPasswordService _passwordService;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserAdminRootCommandHandler(IUserService userService, IPasswordService passwordService,
        ILogger<CreateUserCommandHandler> logger)
    {
        _userService = userService;
        _passwordService = passwordService;
        _logger = logger;
    }

    public async Task<UserItem> Handle(CreateUserAdminRootCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начало создания пользователя с именем {UserName}", request.User.Username);
        var hashedPassword = _passwordService.HashPassword(request.User.Password);
        var user = new User()
        {
            Username = request.User.Username,
            PasswordHash = hashedPassword,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsRemoved = false,
            RoleId = (int)request.User.Role
        };
        var createdUser = await _userService.Create(user);
        _logger.LogInformation("Пользователь с именем {UserName} успешно создан", request.User.Username);
        return createdUser;
    }
}