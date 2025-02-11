using System.ComponentModel.DataAnnotations;

namespace HoneyShop.Model.Models.User;

public class UpdateUser
{
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "Имя пользователя обязательно")]
    public string Username { get; set; }
    
    public string FullName { get; set; } = string.Empty;// Полное имя пользователя
    
    [Phone]
    public string PhoneNumber { get; set; }= string.Empty; // Номер телефона
}