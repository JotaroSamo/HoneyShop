using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Core.Contracts.Auth;
using HoneyShop.Model.Models.Auth;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Users.LoginUser;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, JwtModel>
{
    private readonly IAuthService _authService;
    private readonly ILogger<LoginUserCommandHandler> _logger;

    public LoginUserCommandHandler(IAuthService authService, ILogger<LoginUserCommandHandler> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    public async Task<JwtModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Попытка входа пользователя с именем {Username}", request.Username);
        var jwtModel = await _authService.Authenticate(request.Username, request.Password);
        if (jwtModel != null)
        {
            _logger.LogInformation("Пользователь {Username} успешно вошел в систему", request.Username);
        }
        else
        {
            _logger.LogWarning("Не удалось войти пользователю с именем {Username}", request.Username);
        }
        return jwtModel;
    }
}