using System.ComponentModel;

namespace HoneyShop.Model.Enums;

public enum ProductStatusEnum
{
    [Description("В наличии")]
    InStock = 1,
    [Description("Нет в наличии")]
    OutOfStock = 2,
    [Description("Нет данных")]
    NoData = 3
}