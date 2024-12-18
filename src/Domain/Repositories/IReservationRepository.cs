public interface IReservationRepository : IRepository<Reservation>
{
    Task<IEnumerable<Reservation>> GetReservationsByGuestIdAsync(Guid guestId);
    Task<IEnumerable<Reservation>> GetActiveReservationsAsync();
}