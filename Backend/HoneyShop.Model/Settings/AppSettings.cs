using HoneyShop.Model.Settings.Auth;
using HoneyShop.Model.Settings.Cors;

namespace HoneyShop.Model.Settings;

public class AppSettings
{

    public JwtSettings JwtSettings { get; set; }
    public CorsSettings CorsSettings { get; set; }
}