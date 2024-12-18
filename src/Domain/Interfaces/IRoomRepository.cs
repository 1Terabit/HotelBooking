namespace HotelBooking.Domain.Interfaces
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut);
        Task<IEnumerable<Room>> GetRoomsByTypeAsync(string roomType);
        Task<bool> IsRoomAvailableAsync(string roomId, DateTime checkIn, DateTime checkOut);
    }
}