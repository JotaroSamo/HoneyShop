using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Model.Models.User;

namespace HoneyShop.Application.Users.CreateUserAdminRoot;

public class CreateUserAdminRootCommand : ICommand<UserItem>
{
    public Model.Models.User.CreateUserAdminRoot User { get; }


    public CreateUserAdminRootCommand(Model.Models.User.CreateUserAdminRoot user)
    {
        User = user;
    }
}