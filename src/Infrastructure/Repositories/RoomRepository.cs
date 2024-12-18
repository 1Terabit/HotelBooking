using MongoDB.Driver;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Interfaces;

namespace HotelBooking.Infrastructure.Repositories.MongoDb;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    private readonly IMongoCollection<Room> _rooms;

    public RoomRepository(MongoDbContext context) : base(context, "Rooms")
    {
        _rooms = context.Rooms;
    }

    public async Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(string hotelId)
    {
        return await _rooms.Find(r => r.HotelId == hotelId).ToListAsync();
    }

    public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(string hotelId, DateTime checkIn, DateTime checkOut)
    {
        // Primero obtenemos todas las reservaciones que se solapan con las fechas
        var reservationFilter = Builders<Reservation>.Filter.And(
            Builders<Reservation>.Filter.Lte(r => r.CheckInDate, checkOut),
            Builders<Reservation>.Filter.Gte(r => r.CheckOutDate, checkIn)
        );

        //TODO: Obtenemos los IDs de las habitaciones reservadas
        var reservedRoomIds = await _rooms
            .Database
            .GetCollection<Reservation>("Reservations")
            .Distinct<string>(r => r.RoomId, reservationFilter)
            .ToListAsync();

        //TODO: Buscamos las habitaciones disponibles
        var roomFilter = Builders<Room>.Filter.And(
            Builders<Room>.Filter.Eq(r => r.HotelId, hotelId),
            Builders<Room>.Filter.Nin(r => r.Id, reservedRoomIds)
        );

        return await _rooms.Find(roomFilter).ToListAsync();
    }

    public async Task<IEnumerable<Room>> GetRoomsByTypeAsync(string hotelId, string roomType)
    {
        var filter = Builders<Room>.Filter.And(
            Builders<Room>.Filter.Eq(r => r.HotelId, hotelId),
            Builders<Room>.Filter.Eq(r => r.Type, roomType)
        );

        return await _rooms.Find(filter).ToListAsync();
    }
}
