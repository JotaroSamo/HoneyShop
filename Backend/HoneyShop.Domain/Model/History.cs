namespace HonaeyShop.Domain.Model;

public class History
{
    public int Id { get; set; }
    public User User { get; set; }

    public string Message { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
}