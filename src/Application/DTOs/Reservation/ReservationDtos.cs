public class ReservationDto
{
    public string Id { get; set; }
    public string GuestId { get; set; }
    public string RoomId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }

    // Propiedades de navegación (opcional)
    public GuestDto Guest { get; set; }
    public RoomDto Room { get; set; }
}