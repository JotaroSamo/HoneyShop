using HonaeyShop.Domain.Model;
using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Core.Contracts.Security;
using HoneyShop.Core.Contracts.User;
using HoneyShop.Model.Models.User;
using Microsoft.Extensions.Logging;
using Role = HoneyShop.Model.Enums.Role;

namespace HoneyShop.Application.Users.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserItem>
{
    private readonly IUserService _userService;
    private readonly IPasswordService _passwordService;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(IUserService userService, IPasswordService passwordService, ILogger<CreateUserCommandHandler> logger)
    {
        _userService = userService;
        _passwordService = passwordService;
        _logger = logger;
    }

    public async Task<UserItem> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начало создания пользователя с именем {UserName}", request.CreateUser.Username);
        var hashedPassword = _passwordService.HashPassword(request.CreateUser.Password);
        var user = new User()
        {
            PhoneNumber = request.CreateUser.PhoneNumber,
            FullName = request.CreateUser.FullName,
            Username = request.CreateUser.Username,
            PasswordHash = hashedPassword,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsRemoved = false,
            RoleId = (int)Role.User
        };
        var createdUser = await _userService.Create(user);
        _logger.LogInformation("Пользователь с именем {UserName} успешно создан", request.CreateUser.Username);
        return createdUser;
    }
}