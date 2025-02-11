using HonaeyShop.Domain.Model;
using HoneyShop.Model.Models.File;
using Microsoft.AspNetCore.Http;

namespace HoneyShop.Core.Contracts.File;

public interface IFileService
{
    public Task<int> Upload(IFormFile file);
    public Task<List<int>> Upload(List<IFormFile> files);
    public Task<FileItem> Download(int id);

    public Task<bool> Delete(int id);
}