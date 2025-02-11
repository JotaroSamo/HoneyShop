using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Core.Contracts.File;
using HoneyShop.Model.Models.File;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Files.DownloadFile;

public class DownloadFileQueryHandler : IQueryHandler<DownloadFileQuery, FileItem>
{
    private readonly IFileService _fileService;
    private readonly ILogger<DownloadFileQueryHandler> _logger;

    public DownloadFileQueryHandler(IFileService fileService, ILogger<DownloadFileQueryHandler> logger)
    {
        _fileService = fileService;
        _logger = logger;
    }

    public async Task<FileItem> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начало загрузки файла с ID {FileId}", request.Id);
        var file = await _fileService.Download(request.Id);
        if (file != null)
        {
            _logger.LogInformation("Файл с ID {FileId} успешно загружен", request.Id);
        }
        else
        {
            _logger.LogWarning("Не удалось загрузить файл с ID {FileId}", request.Id);
        }
        return file;
    }
}