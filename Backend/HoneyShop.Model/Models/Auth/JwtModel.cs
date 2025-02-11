namespace HoneyShop.Model.Models.Auth;

public class JwtModel
{
    public string Token { get; set; }
    public DateTime ExpiredAt { get; set; }
}