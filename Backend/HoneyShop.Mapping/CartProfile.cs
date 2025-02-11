using AutoMapper;
using HonaeyShop.Domain.Model;
using HoneyShop.Model.Models.Cart;

namespace HoneyShop.Mapping;

public class CartProfile :Profile
{
    public CartProfile()
    {
        CreateMap<Cart, CartItem>().ForMember(x=>x.FileIds, 
            opt=>opt.MapFrom(x=>
                x.Product.Files.Select(y=>y.Id))).ReverseMap();
    }
}