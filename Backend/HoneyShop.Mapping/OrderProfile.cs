using AutoMapper;
using HonaeyShop.Domain.Model;
using HoneyShop.Model.Models.Order;

namespace HoneyShop.Mapping;

public class OrderProfile :Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderItem>()
            .ForMember(x=>x.OrderDetails, opt=>
                opt.MapFrom(x=>x.OrderDetails))
            .ForMember(x=>x.Status, opt=>opt.MapFrom(x=>x.Status.Name))
            .ReverseMap();
    }
}