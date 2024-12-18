public class HotelRepository : Repository<Hotel>, IHotelRepository
{
    public HotelRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Hotel?> GetHotelWithRoomsAsync(Guid id)
    {
        return await _context.Hotels
            .Include(h => h.Rooms)
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<IEnumerable<Hotel>> GetActiveHotelsAsync()
    {
        return await _context.Hotels
            .Where(h => h.IsActive)
            .ToListAsync();
    }
}
