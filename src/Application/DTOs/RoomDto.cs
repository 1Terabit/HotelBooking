public record RoomDto(
    Guid Id,
    string Number,
    string Type,
    decimal PricePerNight,
    bool IsAvailable,
    Guid HotelId
);

public record CreateRoomDto(
    string Number,
    string Type,
    decimal PricePerNight,
    Guid HotelId
);