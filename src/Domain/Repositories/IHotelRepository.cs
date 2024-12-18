public interface IHotelRepository : IRepository<Hotel>
{
    Task<Hotel?> GetHotelWithRoomsAsync(Guid id);
    Task<IEnumerable<Hotel>> GetActiveHotelsAsync();
}
