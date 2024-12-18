public record HotelDto(
    Guid Id,
    string Name,
    string Description,
    string Address,
    string City,
    string Country,
    int Rating,
    ICollection<RoomDto> Rooms
);

public record CreateHotelDto(
    string Name,
    string Description,
    string Address,
    string City,
    string Country,
    int Rating
);