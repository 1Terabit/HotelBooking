using MongoDB.Driver;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Interfaces;

namespace HotelBooking.Infrastructure.Repositories.MongoDb;

public class HotelRepository : IHotelRepository
{
    private readonly MongoDbContext _context;
    private readonly IMongoCollection<Hotel> _hotels;

    public HotelRepository(MongoDbContext context)
    {
        _context = context;
        _hotels = _context.Hotels;
    }

    public async Task<IEnumerable<Hotel>> GetAllAsync()
    {
        return await _hotels.Find(_ => true).ToListAsync();
    }

    public async Task<Hotel> GetByIdAsync(string id)
    {
        return await _hotels.Find(h => h.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Hotel> CreateAsync(Hotel hotel)
    {
        await _hotels.InsertOneAsync(hotel);
        return hotel;
    }

    public async Task UpdateAsync(string id, Hotel hotel)
    {
        await _hotels.ReplaceOneAsync(h => h.Id == id, hotel);
    }

    public async Task DeleteAsync(string id)
    {
        await _hotels.DeleteOneAsync(h => h.Id == id);
    }
}
