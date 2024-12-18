using MongoDB.Driver;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Interfaces;

namespace HotelBooking.Infrastructure.Repositories.MongoDb;

public class GuestRepository : Repository<Guest>, IGuestRepository
{
    private readonly IMongoCollection<Guest> _guests;

    public GuestRepository(MongoDbContext context) : base(context, "Guests")
    {
        _guests = context.Guests;
    }

    public async Task<Guest> GetByEmailAsync(string email)
    {
        return await _guests.Find(g => g.Email == email).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Guest>> GetGuestsByNameAsync(string searchTerm)
    {
        var filter = Builders<Guest>.Filter.Or(
            Builders<Guest>.Filter.Regex(g => g.FirstName, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")),
            Builders<Guest>.Filter.Regex(g => g.LastName, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"))
        );

        return await _guests.Find(filter).ToListAsync();
    }
}
