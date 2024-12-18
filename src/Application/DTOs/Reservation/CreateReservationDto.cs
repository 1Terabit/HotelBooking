public class CreateReservationDto
{
    public string GuestId { get; set; }
    public string RoomId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
}
