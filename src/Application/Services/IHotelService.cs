public interface IHotelService
{
    Task<IEnumerable<HotelDto>> GetAllHotelsAsync();
    Task<HotelDto?> GetHotelByIdAsync(Guid id);
    Task<HotelDto> CreateHotelAsync(CreateHotelDto createHotelDto);
    Task UpdateHotelAsync(Guid id, CreateHotelDto updateHotelDto);
    Task DeleteHotelAsync(Guid id);
    Task<IEnumerable<HotelDto>> GetHotelsByLocationAsync(string city, string country);
}