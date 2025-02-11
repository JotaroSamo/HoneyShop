using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Model.Models.User;

namespace HoneyShop.Application.Users.ChangePassword;

public class ChangePasswordCommand : ICommand<UserItem>
{
    public UpdatePasswordUser PasswordUser { get; }


    public ChangePasswordCommand(UpdatePasswordUser passwordUser)
    {
        PasswordUser = passwordUser;
    }
}