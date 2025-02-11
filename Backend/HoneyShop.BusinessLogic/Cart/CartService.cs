using AutoMapper;
using HoneyShop.Core.Contracts.Cart;
using HoneyShop.Core.Excpetions;
using HoneyShop.DataAccess.Context;
using HoneyShop.Model.Models.Cart;
using HoneyShop.Model.Pagination;
using Microsoft.EntityFrameworkCore;


namespace HoneyShop.BusinessLogic.Cart;

public class CartService : ICartService
{
    private readonly HoneyShopDbContext _context;
    private readonly IMapper _mapper;

    public CartService(HoneyShopDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CartItem> Create(CreateCart cart, int userId)
    {
        try
        {
            var newCart = new HonaeyShop.Domain.Model.Cart()
            {
                UserId = userId,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
                CreatedAt = DateTime.UtcNow // Добавил текущее время
            };
            await _context.Carts.AddAsync(newCart);
            await _context.SaveChangesAsync();
            return _mapper.Map<CartItem>(newCart);
        }
        catch (DbUpdateConcurrencyException e)
        {
            throw new HoneyException("Ошибка конкурентного обновления!", 409);
        }
        catch (DbUpdateException e)
        {
            throw new HoneyException("Ошибка при обновлении базы данных!", 500);
        }
        catch (ArgumentNullException e)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (InvalidOperationException e)
        {
            throw new HoneyException("Недопустимая операция!", 400);
        }
        catch (Exception e)
        {
            throw new HoneyException("Неизвестная ошибка!", 500);
        }
    }

    public async Task<PaginationListModel<CartItem>> GetCartItems(int userId, int page, int pageSize)
    {
        try
        {
            var query = _context.Carts
                .Where(c => c.UserId == userId)
                .Include(c => c.Product).ThenInclude(x => x.Files); // Include для Product

            var totalCount = await query.CountAsync();

            var cartItems = await _mapper.ProjectTo<CartItem>(query).ToListAsync();

            return new PaginationListModel<CartItem>()
            {
                Page = page,
                Size = pageSize,
                Total = totalCount,
                Models = cartItems
            };
        }
        catch (DbUpdateConcurrencyException e)
        {
            throw new HoneyException("Ошибка конкурентного обновления!", 409);
        }
        catch (DbUpdateException e)
        {
            throw new HoneyException("Ошибка при обновлении базы данных!", 500);
        }
        catch (ArgumentNullException e)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (InvalidOperationException e)
        {
            throw new HoneyException("Недопустимая операция!", 400);
        }
        catch (Exception e)
        {
            throw new HoneyException("Неизвестная ошибка!", 500);
        }
    }

    public async Task<bool> DeleteCartItem(int id, int updateUserId)
    {
        try
        {
            await _context.Carts.Where(x => x.Id == id && x.UserId == updateUserId).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();

            return true;
        }
        catch (DbUpdateConcurrencyException e)
        {
            throw new HoneyException("Ошибка конкурентного обновления!", 409);
        }
        catch (DbUpdateException e)
        {
            throw new HoneyException("Ошибка при обновлении базы данных!", 500);
        }
        catch (ArgumentNullException e)
        {
            throw new HoneyException("Передан null аргумент!", 400);
        }
        catch (InvalidOperationException e)
        {
            throw new HoneyException("Недопустимая операция!", 400);
        }
        catch (Exception e)
        {
            throw new HoneyException("Неизвестная ошибка!", 500);
        }
    }
}
