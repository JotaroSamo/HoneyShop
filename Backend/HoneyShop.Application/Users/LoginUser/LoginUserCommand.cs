using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Model.Models.Auth;

namespace HoneyShop.Application.Users.LoginUser;

public class LoginUserCommand : ICommand<JwtModel>
{
    public LoginUserCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; }
    
    public string Password { get; }
}