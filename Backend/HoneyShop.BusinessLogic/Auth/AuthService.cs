using HoneyShop.Core.Contracts.Auth;
using HoneyShop.Core.Contracts.Security;
using HoneyShop.Core.Contracts.User;
using HoneyShop.Core.Excpetions;
using HoneyShop.Model.Models.Auth;

namespace HoneyShop.BusinessLogic.Auth;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IPasswordService _passwordService;

    public AuthService(IUserService userService, ITokenService tokenService, IPasswordService passwordService)
    {
        _userService = userService;
        _tokenService = tokenService;
        _passwordService = passwordService;
    }

    public async Task<JwtModel> Authenticate(string username, string password)
    {
        try
        {
            var user = await _userService.GetByLoginForAuth(username);
            var result = _passwordService.VerifyPassword(user.PasswordHash, password);
            if (!result)
            {
                throw new HoneyException("Неверный пароль", 401);
            }

            var token = _tokenService.GenerateToken(user);
            return token;
        }
        catch (ArgumentNullException e)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (InvalidOperationException e)
        {
            throw new HoneyException("Недопустимая операция!", 400);
        }
        catch (Exception e)
        {
            throw new HoneyException("Неизвестная ошибка!", 500);
        }
    }
}
