using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Core.Contracts.File;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Files.UploadFile;

public class UploadFileCommandHandler : ICommandHandler<UploadFileCommand, List<int>>
{
    private readonly IFileService _fileService;
    private readonly ILogger<UploadFileCommandHandler> _logger;

    public UploadFileCommandHandler(IFileService fileService, ILogger<UploadFileCommandHandler> logger)
    {
        _fileService = fileService;
        _logger = logger;
    }

    public async Task<List<int>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начало загрузки файлов");
        var ids = await _fileService.Upload(request.Files);
        if (ids != null && ids.Count > 0)
        {
            _logger.LogInformation("Файлы успешно загружены с ID: {FileIds}", string.Join(", ", ids));
        }
        else
        {
            _logger.LogWarning("Не удалось загрузить файлы");
        }

        return ids;
    }
}