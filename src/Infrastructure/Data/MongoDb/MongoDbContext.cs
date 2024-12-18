using MongoDB.Driver;
using HotelBooking.Domain.Entities;
using Microsoft.Extensions.Options;

namespace HotelBooking.Infrastructure.Data.MongoDb;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<Hotel> Hotels => _database.GetCollection<Hotel>("Hotels");
    public IMongoCollection<Room> Rooms => _database.GetCollection<Room>("Rooms");
    public IMongoCollection<Guest> Guests => _database.GetCollection<Guest>("Guests");
    public IMongoCollection<Reservation> Reservations => _database.GetCollection<Reservation>("Reservations");
}
