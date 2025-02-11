using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Model.Models.User;

namespace HoneyShop.Application.Users.ChangePasswordAdmin;

public class ChangePasswordAdminCommand : ICommand<UserItem>
{
    public UpdatePasswordUser PasswordUser { get; }


    public ChangePasswordAdminCommand(UpdatePasswordUser passwordUser)
    {
        PasswordUser = passwordUser;
    }
}