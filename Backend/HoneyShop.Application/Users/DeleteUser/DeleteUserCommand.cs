using HoneyShop.Application.Core.Commands.Contracts;

namespace HoneyShop.Application.Users.DeleteUser;

public class DeleteUserCommand : ICommand<bool>
{
    public int Id { get; }

    public DeleteUserCommand(int id)
    {
        Id = id;
    }
}