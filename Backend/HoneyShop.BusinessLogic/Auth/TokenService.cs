using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HoneyShop.Core.Contracts.Auth;
using HoneyShop.Model.Models.Auth;
using HoneyShop.Model.Models.User;
using HoneyShop.Model.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace HoneyShop.BusinessLogic.Auth;

public class TokenService : ITokenService
{
    private readonly AppSettings _settings;


    public TokenService(IOptions<AppSettings> settings)
    {
        _settings = settings.Value;
    }

    public JwtModel GenerateToken(AuthUser user)
    {
       var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSettings.SecretKey));
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.RoleName), // Добавление роли пользователя
        };

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var time = DateTime.Now.AddMinutes(Convert.ToDouble(_settings.JwtSettings.ExpirationMinutes));
        var token = new JwtSecurityToken(
            issuer: _settings.JwtSettings.Issuer,
            audience: _settings.JwtSettings.Audience,
            claims: claims,
            expires: time,
            signingCredentials: credentials
        );
        var jwtModel = new JwtModel()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiredAt = time
        };
        return jwtModel;
    }
}