using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelBooking.Domain.Entities;
public class Room
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("number")]
    public string Number { get; set; }

    [BsonElement("type")]
    public string Type { get; set; }

    [BsonElement("price")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }

    [BsonElement("isAvailable")]
    public bool IsAvailable { get; set; }
}