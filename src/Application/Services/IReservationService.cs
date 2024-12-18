public interface IReservationService
{
    Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
    Task<ReservationDto?> GetReservationByIdAsync(Guid id);
    Task<ReservationDto> CreateReservationAsync(CreateReservationDto createReservationDto);
    Task UpdateReservationAsync(Guid id, CreateReservationDto updateReservationDto);
    Task DeleteReservationAsync(Guid id);
    Task<IEnumerable<ReservationDto>> GetReservationsByGuestIdAsync(Guid guestId);
    Task<IEnumerable<ReservationDto>> GetActiveReservationsAsync();
}