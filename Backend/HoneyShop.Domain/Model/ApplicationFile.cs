namespace HonaeyShop.Domain.Model;

public class ApplicationFile 
{
    public int Id { get; set; } // Уникальный идентификатор файла
    public byte[] Content { get; set; } // Данные файла
    public string Name { get; set; } = string.Empty; // Имя файла
    public long Length { get; set; } // Размер файла в байтах
    public string ContentType { get; set; } = string.Empty;// MIME-тип файла (например, image/jpeg)
    public DateTime CreatedAt { get; set; } // Дата создания файла
    public int CreatedByUserId { get; set; } // Идентификатор пользователя, загрузившего файл
    public User CreatedBy { get; set; } // Связь с пользователем
    public int? ProductId { get; set; } // Связь с продуктом
    public Product? Product { get; set; } // Обратная связь
}
