public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByGuestIdAsync(Guid guestId)
    {
        return await _context.Reservations
            .Where(r => r.GuestId == guestId)
            .Include(r => r.Room)
                .ThenInclude(r => r.Hotel)
            .Include(r => r.Guest)
            .OrderByDescending(r => r.CheckInDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetActiveReservationsAsync()
    {
        var currentDate = DateTime.UtcNow.Date;
        return await _context.Reservations
            .Where(r => r.CheckOutDate >= currentDate)
            .Include(r => r.Room)
                .ThenInclude(r => r.Hotel)
            .Include(r => r.Guest)
            .OrderBy(r => r.CheckInDate)
            .ToListAsync();
    }

    public override async Task<Reservation?> GetByIdAsync(Guid id)
    {
        return await _context.Reservations
            .Include(r => r.Room)
                .ThenInclude(r => r.Hotel)
            .Include(r => r.Guest)
            .FirstOrDefaultAsync(r => r.Id == id);
    }
}
