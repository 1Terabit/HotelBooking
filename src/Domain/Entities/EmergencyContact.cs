using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelBooking.Domain.Entities;

public class EmergencyContact : IAuditableEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("phoneNumber")]
    public string PhoneNumber { get; set; }

    [BsonElement("relationship")]
    public string Relationship { get; set; }

    [BsonElement("guestId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string GuestId { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime? UpdatedAt { get; set; }
}
