using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Core.Contracts.File;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Files.DeleteFile;

public class DeleteFileCommandHandler : ICommandHandler<DeleteFileCommand, bool>
{
    private readonly IFileService _fileService;
    private readonly ILogger<DeleteFileCommandHandler> _logger;

    public DeleteFileCommandHandler(IFileService fileService, ILogger<DeleteFileCommandHandler> logger)
    {
        _fileService = fileService;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начало удаления файла с ID {FileId}", request.Id);
        var result = await _fileService.Delete(request.Id);
        if (result)
        {
            _logger.LogInformation("Файл с ID {FileId} успешно удален", request.Id);
        }
        else
        {
            _logger.LogWarning("Не удалось удалить файл с ID {FileId}", request.Id);
        }

        return result;
    }
}