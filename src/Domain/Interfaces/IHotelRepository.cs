namespace HotelBooking.Domain.Interfaces
{
    public interface IHotelRepository : IBaseRepository<Hotel>
    {
        Task<IEnumerable<Hotel>> GetHotelsByRatingAsync(int rating);
        Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(string hotelId);
    }
}