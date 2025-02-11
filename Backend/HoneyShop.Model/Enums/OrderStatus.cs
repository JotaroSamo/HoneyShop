using System.ComponentModel;

namespace HoneyShop.Model.Enums;

public enum OrderStatus
{
    [Description("На проверке")] Pending = 1,
    [Description("Одобрен")] Approved = 2,
    [Description("Отклонен")] Rejected = 3
}