using HoneyShop.Core.Excpetions;
using HoneyShop.DataAccess.MongoDb.Models;
using HoneyShop.Model.Pagination;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HoneyShop.DataAccess.MongoDb;

public class MongoContextService
{
    private readonly IMongoCollection<LogModel> _logs;
    public MongoContextService(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("MongoConnection"));
        var database = client.GetDatabase(config["DatabaseName"]);
        _logs = database.GetCollection<LogModel>("logs");
    }
    
    public async Task<PaginationListModel<LogModel>> SearchLogsAsync(string? searchTerm, int page, int pageSize)
    {
        try
        {
            var filter = string.IsNullOrEmpty(searchTerm)
                ? Builders<LogModel>.Filter.Empty
                : Builders<LogModel>.Filter.Or(
                    Builders<LogModel>.Filter.Regex(log => log.MessageTemplate, new BsonRegularExpression(searchTerm, "i")),
                    Builders<LogModel>.Filter.Regex(log => log.RenderedMessage, new BsonRegularExpression(searchTerm, "i")),
                    Builders<LogModel>.Filter.Regex(log => log.Level, new BsonRegularExpression(searchTerm, "i")),
                    Builders<LogModel>.Filter.Regex(log => log.UtcTimestamp, new BsonRegularExpression(searchTerm, "i"))
                );

            var sort = Builders<LogModel>.Sort.Descending(log => log.Timestamp);
            var total = await _logs.Find(filter).Sort(sort).CountDocumentsAsync();
            var logs = await _logs.Find(filter)
                .Sort(sort)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
            var result = new PaginationListModel<LogModel>()
            {
                Page = page,
                Size = pageSize,
                Total = total,
                Models = logs
            };
            return result;
        }
        catch (Exception e)
        {
            throw new HoneyException("Ошибка при поиске логов");
        }
       
    }
    public async Task DeleteOldLogsAsync()
    {
        var filter = Builders<LogModel>.Filter.Lt(log => log.Timestamp, DateTime.UtcNow.AddYears(-1));
        var result = await _logs.DeleteManyAsync(filter);
       
    }
}