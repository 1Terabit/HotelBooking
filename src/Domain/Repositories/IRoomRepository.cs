public interface IRoomRepository : IRepository<Room>
{
    Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut);
    Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(Guid hotelId);
}