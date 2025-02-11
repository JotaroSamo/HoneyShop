namespace HoneyShop.Model.Models.File;

public class FileItem
{
    public int Id { get; set; } // Уникальный идентификатор файла
    public byte[] Content { get; set; } // Данные файла
    public string Name { get; set; } = string.Empty; // Имя файла
    public long Length { get; set; } // Размер файла в байтах
    public string ContentType { get; set; } = string.Empty;// MIME-тип файла (например, image/jpeg)
    public DateTime CreatedAt { get; set; } // Дата создания файла
}