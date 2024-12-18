namespace HotelBooking.Application.Interfaces
{
    public interface IGuestService
    {
        Task<IEnumerable<GuestDto>> GetAllGuestsAsync();
        Task<GuestDto> GetGuestByIdAsync(string id);
        Task<GuestDto> CreateGuestAsync(CreateGuestDto guestDto);
        Task UpdateGuestAsync(string id, UpdateGuestDto guestDto);
        Task DeleteGuestAsync(string id);
        Task<GuestDto> GetGuestByEmailAsync(string email);
        Task<IEnumerable<ReservationDto>> GetGuestReservationsAsync(string guestId);
    }
}