using AutoMapper;
using HonaeyShop.Domain.Model;
using HoneyShop.Core.Contracts.Order;
using HoneyShop.Core.Excpetions;
using HoneyShop.DataAccess.Context;
using HoneyShop.Model.Models.Order;
using HoneyShop.Model.Pagination;
using Microsoft.EntityFrameworkCore;

namespace HoneyShop.BusinessLogic.Order;

public class OrderService : IOrderService
{
    private readonly HoneyShopDbContext _context;
    private readonly IMapper _mapper;

    public OrderService(HoneyShopDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<OrderItem> CreateOrder(HonaeyShop.Domain.Model.Order order, int userId)
    {
        try
        {
            order.UserId = userId;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderItem>(order);
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

    public async Task<PaginationListModel<OrderItem>> GetOrdersByUser(int userId, int page, int pageSize)
    {
        try
        {
            var query = _context.Orders.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedAt).Skip(pageSize * page).Take(pageSize);
            var total = await query.CountAsync();
            var orders = await _mapper.ProjectTo<OrderItem>(query).ToListAsync();
            return new PaginationListModel<OrderItem>()
            {
                Page = page,
                Size = pageSize,
                Models = orders,
                Total = total
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

    public async Task<PaginationListModel<OrderItem>> GetOrdersByProduct(int productId, int page, int pageSize)
    {
        try
        {
            var query = _context.Orders
                .Where(x => x.OrderDetails.Any(od => od.ProductId == productId))
                .OrderByDescending(x => x.CreatedAt)
                .Skip(pageSize * page)
                .Take(pageSize);
            var total = await query.CountAsync();
            var orders = await _mapper.ProjectTo<OrderItem>(query).ToListAsync();
            return new PaginationListModel<OrderItem>()
            {
                Page = page,
                Size = pageSize,
                Models = orders,
                Total = total
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


    public async Task<PaginationListModel<OrderItem>> GetOrders(int page, int pageSize)
    {
        try
        {
            var query = _context.Orders.OrderByDescending(x => x.CreatedAt).Skip(pageSize * page).Take(pageSize);
            var total = await query.CountAsync();
            var orders = await _mapper.ProjectTo<OrderItem>(query).ToListAsync();
            return new PaginationListModel<OrderItem>()
            {
                Page = page,
                Size = pageSize,
                Total = total,
                Models = orders
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

    public async Task<OrderItem> ChangeStatus(int orderId, int statusId, int userId)
    {
        try
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new HoneyException("Заказ не найден", 404);
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new HoneyException("Пользователь не найден", 404);
            }
            var history = new History()
            {
                User = user,
                UpdatedAt = DateTime.UtcNow
            };
            order.Histories.Add(history);
            order.StatusId = statusId;
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderItem>(order);
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

    public async Task<bool> DeleteOrder(int orderId)
    {
        try
        {
            await _context.Orders.Where(x => x.Id == orderId).ExecuteDeleteAsync();
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