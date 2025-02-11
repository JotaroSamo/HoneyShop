using System.ComponentModel.DataAnnotations;

namespace HoneyShop.Model.Models.Auth;

public class LoginModel
{
    [Required(ErrorMessage = "Имя пользователя обязательно")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Пароль обязателен")]
    public string Password { get; set; }
}