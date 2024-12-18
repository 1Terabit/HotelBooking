using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelBooking.Domain.Entities;

public class Hotel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("address")]
    public string Address { get; set; }

    [BsonElement("rating")]
    public int Rating { get; set; }

    [BsonElement("rooms")]
    public List<Room> Rooms { get; set; } = new();
}
