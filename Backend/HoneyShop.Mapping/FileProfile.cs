using AutoMapper;
using HonaeyShop.Domain.Model;
using HoneyShop.Model.Models.File;

namespace HoneyShop.Mapping;

public class FileProfile :Profile
{
    public FileProfile()
    {
        CreateMap<ApplicationFile, FileItem>().ReverseMap();
    }
}