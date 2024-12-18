using MongoDB.Driver;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Interfaces;

namespace HotelBooking.Infrastructure.Repositories.MongoDb;

public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    private readonly IMongoCollection<Reservation> _reservations;

    public ReservationRepository(MongoDbContext context) : base(context, "Reservations")
    {
        _reservations = context.Reservations;
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByGuestIdAsync(string guestId)
    {
        return await _reservations.Find(r => r.GuestId == guestId).ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByRoomIdAsync(string roomId)
    {
        return await _reservations.Find(r => r.RoomId == roomId).ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetReservationsForDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var filter = Builders<Reservation>.Filter.And(
            Builders<Reservation>.Filter.Lte(r => r.CheckInDate, endDate),
            Builders<Reservation>.Filter.Gte(r => r.CheckOutDate, startDate)
        );

        return await _reservations.Find(filter).ToListAsync();
    }
}
