using AutoMapper;
using HonaeyShop.Domain.Model;
using HoneyShop.Model.Models.OrderDetails;

namespace HoneyShop.Mapping;

public class OrderDetailProfile :Profile
{
    public OrderDetailProfile()
    {
        CreateMap<OrderDetail, OrderDetailsItem>().ReverseMap();
    }
}