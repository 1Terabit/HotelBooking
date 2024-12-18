public interface IGuestService
{
    Task<IEnumerable<GuestDto>> GetAllGuestsAsync();
    Task<GuestDto?> GetGuestByIdAsync(Guid id);
    Task<GuestDto> CreateGuestAsync(CreateGuestDto createGuestDto);
    Task UpdateGuestAsync(Guid id, CreateGuestDto updateGuestDto);
    Task DeleteGuestAsync(Guid id);
    Task<GuestDto?> GetGuestByEmailAsync(string email);
}