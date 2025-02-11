namespace HonaeyShop.Domain.Model;

// Модель роли
public class Role
{
    public int Id { get; set; } // Идентификатор роли
    public string Name { get; set; } = string.Empty;// Название роли (Admin, Manager, Customer)
    public ICollection<User> Users { get; set; } // Связь с пользователями
}