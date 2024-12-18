public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAllRoomsAsync();
    Task<RoomDto?> GetRoomByIdAsync(Guid id);
    Task<RoomDto> CreateRoomAsync(CreateRoomDto createRoomDto);
    Task UpdateRoomAsync(Guid id, CreateRoomDto updateRoomDto);
    Task DeleteRoomAsync(Guid id);
    Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut);
    Task<IEnumerable<RoomDto>> GetRoomsByHotelIdAsync(Guid hotelId);
}