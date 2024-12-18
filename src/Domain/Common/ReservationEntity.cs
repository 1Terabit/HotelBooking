using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using HotelBooking.Domain.Enums;

namespace HotelBooking.Domain.Entities;

public class Room : IAuditableEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("number")]
    public string Number { get; set; }

    [BsonElement("type")]
    [BsonRepresentation(BsonType.String)]
    public RoomType Type { get; set; }

    [BsonElement("price")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }

    [BsonElement("isAvailable")]
    public bool IsAvailable { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime? UpdatedAt { get; set; }
}
