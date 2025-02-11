using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Model.Enums;
using HoneyShop.Model.Models.User;

namespace HoneyShop.Application.Users.ChangeRoleAdmin;

public class ChangeRoleAdminCommand : ICommand<UserItem>
{
    public int Id { get; }
    public Role Role { get; }

    public ChangeRoleAdminCommand(int id, Role role)
    {
        Id = id;
        Role = role;
    }
}