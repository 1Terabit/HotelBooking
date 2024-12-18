// Application/DTOs/CreateHotelDto.cs
namespace HotelBooking.Application.DTOs
{
    public class CreateHotelDto
    {
        public required string Name { get; set; }
        public required string Location { get; set; }
    }

}
