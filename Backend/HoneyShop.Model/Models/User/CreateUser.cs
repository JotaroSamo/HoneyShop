using System.ComponentModel.DataAnnotations;

namespace HoneyShop.Model.Models.User;

public class CreateUser
{
    [Required(ErrorMessage = "Имя пользователя обязательно")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Полное имя пользователя обязательно")]
    public string FullName { get; set; } = string.Empty;// Полное имя пользователя
    
    [Required(ErrorMessage = "Номер обязателен")]
    [Phone]
    public string PhoneNumber { get; set; }= string.Empty; // Номер телефона

    [Required(ErrorMessage = "Пароль обязателен")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Подтверждение пароля обязательно")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; }
}