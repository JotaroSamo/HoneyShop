using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HoneyShop.DataAccess.MongoDb.Models;

public class LogModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("MessageTemplate")]
    public string MessageTemplate { get; set; }
    
    [BsonElement("RenderedMessage")]
    public string RenderedMessage { get; set; }
    
    
    [BsonElement("Level")]
    public string Level { get; set; }
    
    [BsonElement("Properties")]
   
    public Object Properties { get; set; }


    [BsonElement("Timestamp")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Timestamp { get; set; }
    
    [BsonElement("UtcTimestamp")]
    
    [BsonRepresentation(BsonType.String)]
    public string UtcTimestamp { get; set; }


}