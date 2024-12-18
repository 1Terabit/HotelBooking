public class GuestRepository : Repository<Guest>, IGuestRepository
{
    public GuestRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Guest?> GetGuestByEmailAsync(string email)
    {
        return await _context.Guests
            .Include(g => g.Reservations)
                .ThenInclude(r => r.Room)
                    .ThenInclude(r => r.Hotel)
            .FirstOrDefaultAsync(g => g.Email == email);
    }

    public override async Task<Guest?> GetByIdAsync(Guid id)
    {
        return await _context.Guests
            .Include(g => g.Reservations)
                .ThenInclude(r => r.Room)
            .FirstOrDefaultAsync(g => g.Id == id);
    }
}
