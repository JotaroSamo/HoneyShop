using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Model.Models.File;

namespace HoneyShop.Application.Files.DownloadFile;

public class DownloadFileQuery: IQuery<FileItem>
{
    public int Id { get; }

    public DownloadFileQuery(int id)
    {
        Id = id;
    }
}