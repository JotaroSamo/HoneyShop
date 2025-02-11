using AutoMapper;
using HonaeyShop.Domain.Model;
using HoneyShop.Core.Contracts.User;
using HoneyShop.Core.Excpetions;
using HoneyShop.DataAccess.Context;
using HoneyShop.Model.Models.User;
using HoneyShop.Model.Pagination;
using Microsoft.EntityFrameworkCore;

namespace HoneyShop.BusinessLogic.User;

public class UserService : IUserService
{
    private readonly HoneyShopDbContext _context;
    private readonly IMapper _mapper;


    public UserService(HoneyShopDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserItem> Create(HonaeyShop.Domain.Model.User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<HonaeyShop.Domain.Model.User, UserItem>(user);
        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new HoneyException("Ошибка конкурентного обновления!", 409);
        }
        catch (DbUpdateException exception)
        {
            throw new HoneyException("Возможно такой пользователь существует!", 400);
        }
        catch (ArgumentNullException exception)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (InvalidOperationException exception)
        {
            throw new HoneyException("Недопустимая операция!", 400);
        }
        catch (Exception exception)
        {
            throw new HoneyException("Ошибка при создании пользователя.", 500);
        }
    }

    public async Task<UserItem> GetByLogin(string login)
    {
        try
        {
            var query = _context.Users.Where(x => x.Username == login && x.IsRemoved == false)
                .Include(r => r.Role);
            var user = await _mapper.ProjectTo<UserItem>(query).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new HoneyException("Пользователя не существует!", 404);
            }

            return user;
        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new HoneyException("Ошибка конкурентного обновления!", 409);
        }
        catch (DbUpdateException exception)
        {
            throw new HoneyException("Ошибка при поиске пользователя.", 500);
        }
        catch (ArgumentNullException exception)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (InvalidOperationException exception)
        {
            throw new HoneyException("Недопустимая операция!", 400);
        }
        catch (Exception exception)
        {
            throw new HoneyException("Ошибка при поиске пользователя.", 500);
        }
    }

    public async Task<AuthUser> GetByIdForUpdatePassword(int id)
    {
        try
        {
            var query = _context.Users.Where(x => x.Id == id && x.IsRemoved == false);
            var user = await _mapper.ProjectTo<AuthUser>(query).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new HoneyException("Пользователя не существует!", 404);
            }

            return user;
        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new HoneyException("Ошибка конкурентного обновления!", 409);
        }
        catch (DbUpdateException exception)
        {
            throw new HoneyException("Ошибка при поиске пользователя.", 500);
        }
        catch (ArgumentNullException exception)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (InvalidOperationException exception)
        {
            throw new HoneyException("Недопустимая операция!", 400);
        }
        catch (Exception exception)
        {
            throw new HoneyException("Ошибка при поиске пользователя.", 500);
        }
    }

      public async Task<AuthUser> GetByLoginForAuth(string login)
    {
        try
        {
            var query = _context.Users.Where(x => x.Username == login && x.IsRemoved == false);
            var user = await _mapper.ProjectTo<AuthUser>(query).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new HoneyException("Пользователя не существует!", 404);
            }

            return user;
        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new HoneyException("Ошибка конкурентного обновления!", 409);
        }
        catch (DbUpdateException exception)
        {
            throw new HoneyException("Ошибка при обновлении базы данных!", 500);
        }
        catch (ArgumentNullException exception)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (InvalidOperationException exception)
        {
            throw new HoneyException("Недопустимая операция!", 400);
        }
        catch (Exception exception)
        {
            throw new HoneyException("Ошибка при поиске пользователя.", 500);
        }
    }

    public async Task<UserItem> Update(UpdateUser updateUser, int updateIdUser)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == updateUser.Id);
            if (user == null)
            {
                throw new HoneyException("Пользователя не существует!", 404);
            }
            var userUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Id == updateIdUser);
            if (userUpdate == null)
            {
                throw new HoneyException("Пользователя не существует!", 404);
            }
            var history = new History()
            {
                User = userUpdate,
                UpdatedAt = DateTime.UtcNow,
                Message = "Обновление пользователя"
            };
            user.Histories.Add(history);
            user.FullName = updateUser.FullName;
            user.PhoneNumber = updateUser.PhoneNumber;
            user.Username = updateUser.Username;

            await _context.SaveChangesAsync();
            return _mapper.Map<UserItem>(user);
        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new HoneyException("Ошибка конкурентного обновления!", 409);
        }
        catch (DbUpdateException exception)
        {
            throw new HoneyException("Ошибка при обновлении базы данных!", 500);
        }
        catch (ArgumentNullException exception)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (InvalidOperationException exception)
        {
            throw new HoneyException("Недопустимая операция!", 400);
        }
        catch (Exception exception)
        {
            throw new HoneyException("Ошибка при поиске пользователя.", 500);
        }
    }

    public async Task<UserItem> ChangeRoleAdmin(int id, int role, int updateIdUser)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new HoneyException("Пользователя не существует!", 404);
            }
            var userUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Id == updateIdUser);
            if (userUpdate == null)
            {
                throw new HoneyException("Пользователя не существует!", 404);
            }
            var history = new History()
            {
                User = userUpdate,
                UpdatedAt = DateTime.UtcNow,
                Message = "Обновление роли"
            };
            user.Histories.Add(history);
            user.RoleId = role;
            await _context.SaveChangesAsync();
            return _mapper.Map<UserItem>(user);
        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new HoneyException("Ошибка конкурентного обновления!", 409);
        }
        catch (DbUpdateException exception)
        {
            throw new HoneyException("Ошибка при обновлении базы данных!", 500);
        }
        catch (ArgumentNullException exception)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (InvalidOperationException exception)
        {
            throw new HoneyException("Недопустимая операция!", 400);
        }
        catch (Exception exception)
        {
            throw new HoneyException("Ошибка при поиске пользователя.", 500);
        }
    }

    public async Task<UserItem> ChangePassword(int id, string hashPassword, int updateIdUser)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new HoneyException("Пользователя не существует!", 404);
            }
            var userUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Id == updateIdUser);
            if (userUpdate == null)
            {
                throw new HoneyException("Пользователя не существует!", 404);
            }
            var history = new History()
            {
                User = userUpdate,
                UpdatedAt = DateTime.UtcNow,
                Message = "Обновление пароля"
            };
            user.Histories.Add(history);
            user.PasswordHash = hashPassword;
            await _context.SaveChangesAsync();
            return _mapper.Map<UserItem>(user);
        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new HoneyException("Ошибка конкурентного обновления!", 409);
        }
        catch (DbUpdateException exception)
        {
            throw new HoneyException("Ошибка при обновлении базы данных!", 500);
        }
        catch (ArgumentNullException exception)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (InvalidOperationException exception)
        {
            throw new HoneyException("Недопустимая операция!", 400);
        }
        catch (Exception exception)
        {
            throw new HoneyException("Ошибка при поиске пользователя.", 500);
        }
    }

    public async Task<bool> DeleteUser(int id, int updateIdUser)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new HoneyException("Пользователя не существует!", 404);
            }
            var userUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Id == updateIdUser);
            if (userUpdate == null)
            {
                throw new HoneyException("Пользователя не существует!", 404);
            }
            var history = new History()
            {
                User = userUpdate,
                UpdatedAt = DateTime.UtcNow,
                Message = "Удаление пользователя"
            };
            user.Histories.Add(history);
            user.IsRemoved = true;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new HoneyException("Ошибка конкурентного обновления!", 409);
        }
        catch (DbUpdateException exception)
        {
            throw new HoneyException("Ошибка при обновлении базы данных!", 500);
        }
        catch (ArgumentNullException exception)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (InvalidOperationException exception)
        {
            throw new HoneyException("Недопустимая операция!", 400);
        }
        catch (Exception exception)
        {
            throw new HoneyException("Ошибка при поиске пользователя.", 500);
        }
    }

    public async Task<PaginationListModel<UserItem>> GetUsersAdmin(int page, int pageSize)
    {
        try
        {
            var query = _context.Users.Skip(pageSize * page).Take(pageSize);
            var users = await _mapper.ProjectTo<UserItem>(query).ToListAsync();
            var paginationUsers = new PaginationListModel<UserItem>()
            {
                Page = page,
                Size = pageSize,
                Models = users
            };

            return paginationUsers;
        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new HoneyException("Ошибка конкурентного обновления!", 409);
        }
        catch (DbUpdateException exception)
        {
            throw new HoneyException("Ошибка при обновлении базы данных!", 500);
        }
        catch (ArgumentNullException exception)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (InvalidOperationException exception)
        {
            throw new HoneyException("Недопустимая операция!", 400);
        }
        catch (Exception exception)
        {
            throw new HoneyException("Ошибка при поиске пользователя.", 500);
        }
    }

    public async Task<UserItem> GetUserById(int id)
    {
        try
        {
            var query = _context.Users.Where(x => x.Id == id);
            var user = await _mapper.ProjectTo<UserItem>(query).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new HoneyException("Пользователя не существует!", 404);
            }

            return user;

        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new HoneyException("Ошибка конкурентного обновления!", 409);
        }
        catch (DbUpdateException exception)
        {
            throw new HoneyException("Ошибка при обновлении базы данных!", 500);
        }
        catch (ArgumentNullException exception)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (InvalidOperationException exception)
        {
            throw new HoneyException("Недопустимая операция!", 400);
        }
        catch (Exception exception)
        {
            throw new HoneyException("Ошибка при поиске пользователя.", 500);
        }
    }
}