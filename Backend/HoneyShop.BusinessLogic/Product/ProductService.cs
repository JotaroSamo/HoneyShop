using AutoMapper;
using HonaeyShop.Domain.Model;
using HoneyShop.Core.Contracts.Product;
using HoneyShop.Core.Excpetions;
using HoneyShop.DataAccess.Context;
using HoneyShop.Model.Models.Product;
using HoneyShop.Model.Pagination;
using Microsoft.EntityFrameworkCore;

namespace HoneyShop.BusinessLogic.Product;

public class ProductService : IProductService
{
    private readonly HoneyShopDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(HoneyShopDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ProductItem> Create(HonaeyShop.Domain.Model.Product product, List<int> fileIds)
    {
        try
        {
            var files = await _context.Files.Where(f => fileIds.Contains(f.Id)).ToListAsync();
            product.Files = files;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductItem>(product);
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

    public async Task<PaginationListModel<ProductItem>> GetProductPage(int page, int pageSize)
    {
        try
        {
            var query = _context.Products.OrderBy(x => x.Name).Take(pageSize).Skip(pageSize * page).Where(x => x.IsRemoved == false);
            var products = await _mapper.ProjectTo<ProductItem>(query).ToListAsync();
            var pagination = new PaginationListModel<ProductItem>()
            {
                Page = page,
                Size = pageSize,
                Total = await _context.Products.CountAsync(),
                Models = products
            };
            return pagination;
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

    public async Task<ProductItem> ChangeStatus(int id, int statusId, int updateUserId)
    {
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.IsRemoved == false);
            if (product == null)
            {
                throw new HoneyException("Продукт не найден", 404);
            }
            var updateUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == updateUserId);
            if (updateUser == null)
            {
                throw new HoneyException("Пользователь не найден", 404);
            }
            var history = new History()
            {
                User = updateUser,
                UpdatedAt = DateTime.UtcNow,
                Message = "Обновление статуса"
            };
            product.Histories.Add(history);
            product.StatusId = statusId;
            product.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductItem>(product);
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

    public async Task<ProductItem> Update(UpdateProduct updatedProduct, int updateUserId)
    {
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == updatedProduct.Id && p.IsRemoved == false);
            if (product == null)
            {
                throw new HoneyException("Продукт не найден", 404);
            }
            var files = await _context.Files.Where(f => updatedProduct.Files.Contains(f.Id)).ToListAsync();
            var updateUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == updateUserId);
            if (updateUser == null)
            {
                throw new HoneyException("Пользователь не найден", 404);
            }
            var history = new History()
            {
                User = updateUser,
                UpdatedAt = DateTime.UtcNow,
                Message = "Обновление продукта"
            };
            product.Histories.Add(history);
            product.Files = files;
            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
            product.StatusId = updatedProduct.StatusId;
            product.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductItem>(product);
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

    public async Task<bool> Delete(int id, int updateUserId)
    {
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.IsRemoved == false);
            if (product == null)
            {
                throw new HoneyException("Продукт не найден", 404);
            }
            var updateUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == updateUserId);
            if (updateUser == null)
            {
                throw new HoneyException("Пользователь не найден", 404);
            }
            var history = new History()
            {
                User = updateUser,
                UpdatedAt = DateTime.UtcNow,
                Message = "Удаление продукта"
            };
            product.Histories.Add(history);
            product.IsRemoved = true;
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

    public async Task<ProductItem> GetProductById(int id)
    {
        try
        {
            var query = _context.Products.Where(x => x.Id == id);
            var product = await _mapper.ProjectTo<ProductItem>(query).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new HoneyException("Продукт не найден", 404);
            }

            return product;
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