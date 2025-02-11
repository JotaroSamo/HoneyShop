namespace HonaeyShop.Domain.Model;

// Модель пользователя
public class User
{
    public int Id { get; set; } // Идентификатор пользователя
    public string Username { get; set; } = string.Empty; // Имя пользователя
    public string PasswordHash { get; set; } = string.Empty;// Хэш пароля
    public string FullName { get; set; } = string.Empty;// Полное имя пользователя
    public string PhoneNumber { get; set; }= string.Empty; // Номер телефона
    public long? TelegramId { get; set; } // Уникальный идентификатор Telegram (добавленное поле)
    public int RoleId { get; set; } // Идентификатор роли
    public Role Role { get; set; } // Связь с ролью
    
    public bool IsRemoved { get; set; }
    public DateTime CreatedAt { get; set; } // Дата создания записи
    public DateTime UpdatedAt { get; set; } // Дата обновления записи
    
    public ICollection<History> Histories { get; set; } // Связь с историей изменений
    public ICollection<Order> Orders { get; set; } // Связь с заказами
    public ICollection<Cart> Carts { get; set; } // Связь с корзинами
}