namespace HotelBooking.Domain.Interfaces
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Reservation>> GetActiveReservationsAsync();
        Task<IEnumerable<Reservation>> GetReservationsByRoomIdAsync(string roomId);
        Task<decimal> CalculateTotalPriceAsync(string roomId, DateTime checkIn, DateTime checkOut);
    }
}