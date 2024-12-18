namespace HotelBooking.Application.Interfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelDto>> GetAllHotelsAsync();
        Task<HotelDto> GetHotelByIdAsync(string id);
        Task<HotelDto> CreateHotelAsync(CreateHotelDto hotelDto);
        Task UpdateHotelAsync(string id, UpdateHotelDto hotelDto);
        Task DeleteHotelAsync(string id);
        Task<IEnumerable<HotelDto>> GetHotelsByRatingAsync(int rating);
        Task<IEnumerable<RoomDto>> GetRoomsByHotelIdAsync(string hotelId);
    }
}
