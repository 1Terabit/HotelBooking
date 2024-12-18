public class CreateRoomDto
{
    public string Number { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; } = true;
}