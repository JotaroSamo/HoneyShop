using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Model.Models.User;

namespace HoneyShop.Application.Users.UpdateUserAdmin;

public class UpdateUserAdminCommand : ICommand<UserItem>
{
    public Model.Models.User.UpdateUser UpdateUser { get; }

    public UpdateUserAdminCommand(Model.Models.User.UpdateUser updateUser)
    {
        UpdateUser = updateUser;
    }
}