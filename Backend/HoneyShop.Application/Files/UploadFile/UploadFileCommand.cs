using HoneyShop.Application.Core.Commands.Contracts;
using Microsoft.AspNetCore.Http;

namespace HoneyShop.Application.Files.UploadFile;

public class UploadFileCommand : ICommand<List<int>>
{
    public List<IFormFile> Files { get; }


    public UploadFileCommand(List<IFormFile> files)
    {
        Files = files;
    }
}