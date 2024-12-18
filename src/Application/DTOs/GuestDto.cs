public record GuestDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Phone
);

public record CreateGuestDto(
    string FirstName,
    string LastName,
    string Email,
    string Phone
);