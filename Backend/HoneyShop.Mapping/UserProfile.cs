using AutoMapper;
using HonaeyShop.Domain.Model;
using HoneyShop.Model.Models.User;

namespace HoneyShop.Mapping;

public class UserProfile :Profile
{
    public UserProfile()
    {
        CreateMap<User, UserItem>().ForMember(x=>x.RoleName, 
            opt => opt.MapFrom(x=>x.Role.Name)).ReverseMap();
        CreateMap<User, AuthUser>().ForMember(x=>x.RoleName, 
            opt => opt.MapFrom(x=>x.Role.Name)).ReverseMap();
    }
}