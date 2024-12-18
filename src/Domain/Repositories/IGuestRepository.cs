public interface IGuestRepository : IRepository<Guest>
{
    Task<Guest?> GetGuestByEmailAsync(string email);
}