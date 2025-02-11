using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Model.Models.User;

namespace HoneyShop.Application.Users.CreateUser;

public class CreateUserCommand : ICommand<UserItem>
{
    public Model.Models.User.CreateUser CreateUser { get; }


    public CreateUserCommand(Model.Models.User.CreateUser createUser)
    {
        CreateUser = createUser;
    }
}