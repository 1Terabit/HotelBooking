namespace HotelBooking.Application.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllRoomsAsync();
        Task<RoomDto> GetRoomByIdAsync(string id);
        Task<RoomDto> CreateRoomAsync(CreateRoomDto roomDto);
        Task UpdateRoomAsync(string id, UpdateRoomDto roomDto);
        Task DeleteRoomAsync(string id);
        Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut);
        Task<bool> IsRoomAvailableAsync(string roomId, DateTime checkIn, DateTime checkOut);
        Task<IEnumerable<RoomDto>> GetRoomsByTypeAsync(string roomType);
    }
}