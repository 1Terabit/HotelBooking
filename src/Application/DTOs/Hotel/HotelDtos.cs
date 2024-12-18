public class HotelDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Rating { get; set; }
    public List<RoomDto> Rooms { get; set; } = new();
}