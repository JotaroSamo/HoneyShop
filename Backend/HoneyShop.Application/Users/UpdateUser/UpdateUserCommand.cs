using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Model.Models.User;

namespace HoneyShop.Application.Users.UpdateUser;

public class UpdateUserCommand : ICommand<UserItem>
{
    public Model.Models.User.UpdateUser UpdateUser { get; }

    public UpdateUserCommand(Model.Models.User.UpdateUser updateUser)
    {
        UpdateUser = updateUser;
    }
}