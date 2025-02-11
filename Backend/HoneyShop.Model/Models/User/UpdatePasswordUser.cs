using System.ComponentModel.DataAnnotations;

namespace HoneyShop.Model.Models.User;

public class UpdatePasswordUser
{
    [Required]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Пароль обязателен")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Пароль обязателен")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Подтверждение пароля обязательно")]
    [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; }
}