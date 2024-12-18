namespace HotelBooking.Application.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
        Task<ReservationDto> GetReservationByIdAsync(string id);
        Task<ReservationDto> CreateReservationAsync(CreateReservationDto reservationDto);
        Task UpdateReservationAsync(string id, UpdateReservationDto reservationDto);
        Task DeleteReservationAsync(string id);
        Task<IEnumerable<ReservationDto>> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<ReservationDto>> GetActiveReservationsAsync();
        Task<decimal> CalculateTotalPriceAsync(string roomId, DateTime checkIn, DateTime checkOut);
    }
}