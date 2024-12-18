using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelBooking.Domain.Entities;
public class Reservation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("guestId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string GuestId { get; set; }

    [BsonElement("roomId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string RoomId { get; set; }

    [BsonElement("checkIn")]
    public DateTime CheckIn { get; set; }

    [BsonElement("checkOut")]
    public DateTime CheckOut { get; set; }

    [BsonElement("totalPrice")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal TotalPrice { get; set; }

    [BsonElement("status")]
    public string Status { get; set; }
}
