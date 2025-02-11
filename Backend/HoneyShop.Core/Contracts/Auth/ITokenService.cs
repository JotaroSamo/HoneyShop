using HoneyShop.Model.Models.Auth;
using HoneyShop.Model.Models.User;

namespace HoneyShop.Core.Contracts.Auth;

public interface ITokenService
{
   public JwtModel GenerateToken(AuthUser user);
}