namespace HoneyShop.Model.Models.User;

public class AuthUser
{
    public int Id { get; set; } // Идентификатор пользователя
    public string Username { get; set; } // Полное имя пользователя
    public string RoleName { get; set; } // Название роли пользователя
    public string PasswordHash { get; set; } // Хэш пароля
}