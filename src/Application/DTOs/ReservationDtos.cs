public record ReservationDto(
    Guid Id,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    decimal TotalPrice,
    Guid GuestId,
    Guid RoomId,
    GuestDto Guest,
    RoomDto Room
);

public record CreateReservationDto(
    DateTime CheckInDate,
    DateTime CheckOutDate,
    Guid GuestId,
    Guid RoomId
);