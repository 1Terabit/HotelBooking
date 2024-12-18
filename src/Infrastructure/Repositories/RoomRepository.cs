public class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut)
    {
        return await _context.Rooms
            .Where(r => !r.Reservations.Any(res =>
                (checkIn >= res.CheckInDate && checkIn < res.CheckOutDate) ||
                (checkOut > res.CheckInDate && checkOut <= res.CheckOutDate) ||
                (checkIn <= res.CheckInDate && checkOut >= res.CheckOutDate)))
            .Include(r => r.Hotel)
            .ToListAsync();
    }

    public async Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(Guid hotelId)
    {
        return await _context.Rooms
            .Where(r => r.HotelId == hotelId)
            .Include(r => r.RoomType)
            .ToListAsync();
    }
}
