using AutoMapper;
using HonaeyShop.Domain.Model;
using HoneyShop.Core.Contracts.File;
using HoneyShop.Core.Excpetions;
using HoneyShop.DataAccess.Context;
using HoneyShop.Model.Models.File;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HoneyShop.BusinessLogic.File;

public class FileService : IFileService
{
    private readonly HoneyShopDbContext _context;
    private readonly IMapper _mapper;

    public FileService(HoneyShopDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Upload(IFormFile file)
    {
        try
        {
            var appFile = await SaveToDatabase(file);
            return appFile.Id;
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

    public async Task<List<int>> Upload(List<IFormFile> files)
    {
        try
        {
            var appFiles = await SaveToDatabase(files);
            return appFiles.Select(x => x.Id).ToList();
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

    public async Task<FileItem> Download(int id)
    {
        try
        {
            var query = _context.Files.Where(x => x.Id == id);
            var file = await _mapper.ProjectTo<FileItem>(query).FirstOrDefaultAsync();
            if (file == null)
            {
                throw new HoneyException("Файл не найден", 404);
            }

            return file;
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

    public async Task<bool> Delete(int id)
    {
        try
        {
            await _context.Files
                .Where(f => f.Id == id)
                .ExecuteDeleteAsync();

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

    private async Task<ApplicationFile> SaveToDatabase(IFormFile file)
    {
        var appFile = await CreateAppFile(file);

        await _context.Files.AddAsync(appFile);

        await _context.SaveChangesAsync();

        return appFile;
    }

    private async Task<List<ApplicationFile>> SaveToDatabase(List<IFormFile> files)
    {
        var appFiles = new List<ApplicationFile>();

        foreach (var file in files)
        {
            var appFile = await CreateAppFile(file);

            appFiles.Add(appFile);
            await _context.Files.AddAsync(appFile);
        }

        await _context.SaveChangesAsync();

        return appFiles;
    }

    private async Task<ApplicationFile> CreateAppFile(IFormFile file)
    {
        await using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        return new ApplicationFile
        {
            Content = memoryStream.ToArray(),
            Length = file.Length,
            ContentType = file.ContentType,
            CreatedByUserId = 1,
            Name = file.FileName,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
