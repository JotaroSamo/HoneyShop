using HoneyShop.Model.Models.Auth;

namespace HoneyShop.Core.Contracts.Auth;

public interface IAuthService
{
    Task<JwtModel> Authenticate(string username, string password);
}