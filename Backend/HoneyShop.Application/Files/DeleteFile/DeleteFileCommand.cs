using HoneyShop.Application.Core.Commands.Contracts;

namespace HoneyShop.Application.Files.DeleteFile;

public class DeleteFileCommand : ICommand<bool>
{
    public int Id { get; }

    public DeleteFileCommand(int id)
    {
        Id = id;
    }
}