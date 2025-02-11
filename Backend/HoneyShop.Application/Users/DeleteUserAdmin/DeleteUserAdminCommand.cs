using HoneyShop.Application.Core.Commands.Contracts;

namespace HoneyShop.Application.Users.DeleteUserAdmin;

public class DeleteUserAdminCommand : ICommand<bool>
{
    public int Id { get; }

    public DeleteUserAdminCommand(int id)
    {
        Id = id;
    }
}