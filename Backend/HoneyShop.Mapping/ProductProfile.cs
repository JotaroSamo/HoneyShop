using AutoMapper;
using HonaeyShop.Domain.Model;
using HoneyShop.Model.Models.Product;

namespace HoneyShop.Mapping;

public class ProductProfile :Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductItem>().ForMember(x=>x.Status, 
            opt=>opt.MapFrom(x=>x.Status.Name))
            .ForMember(x=>x.Files, opt=>
                opt.MapFrom(x=>x.Files.Select(x=>x.Id))).ReverseMap();
    }
}