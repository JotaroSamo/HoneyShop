using System.ComponentModel.DataAnnotations;
using HoneyShop.Model.Enums;

namespace HoneyShop.Model.Models.User;

public class CreateUserAdminRoot
{
    [Required(ErrorMessage = "Имя пользователя обязательно")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Пароль обязателен")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Подтверждение пароля обязательно")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; }
    
    public Role Role { get; set; }
}