namespace HotelBooking.Domain.Interfaces
{
    public interface IGuestRepository : IBaseRepository<Guest>
    {
        Task<IEnumerable<Reservation>> GetGuestReservationsAsync(string guestId);
        Task<Guest> GetGuestByEmailAsync(string email);
    }
}