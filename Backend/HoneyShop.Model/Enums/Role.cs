using System.ComponentModel;

namespace HoneyShop.Model.Enums;

public enum Role
{
    [Description("Админ")] Admin = 1,
    [Description("Менеджер")] Manager = 2,
    [Description("Пользователь")] User = 3
}