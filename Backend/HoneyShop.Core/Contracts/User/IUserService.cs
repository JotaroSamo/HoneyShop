using HoneyShop.Model.Models.User;
using HoneyShop.Model.Pagination;

namespace HoneyShop.Core.Contracts.User;

public interface IUserService
{
    public Task<UserItem> Create(HonaeyShop.Domain.Model.User user);
    public Task<UserItem> GetByLogin(string login);
    
    public Task<AuthUser> GetByIdForUpdatePassword(int id);
    
    public Task<AuthUser> GetByLoginForAuth(string login);

    public Task<UserItem> Update(UpdateUser updateUser, int updateIdUser);
    
    public Task<UserItem> ChangeRoleAdmin(int id, int role, int updateIdUser);

    public Task<UserItem> ChangePassword(int id, string hashPassword, int updateIdUser);

    public Task<bool> DeleteUser(int id, int updateIdUser);

    public Task<PaginationListModel<UserItem>> GetUsersAdmin(int page, int pageSize);
    
    public Task<UserItem> GetUserById(int id);

}