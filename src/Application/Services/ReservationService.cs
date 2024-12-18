// Application/Services/ReservationService.cs
public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IMapper _mapper;

    public ReservationService(
        IReservationRepository reservationRepository,
        IRoomRepository roomRepository,
        IGuestRepository guestRepository,
        IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _roomRepository = roomRepository;
        _guestRepository = guestRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
    {
        var reservations = await _reservationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
    }

    public async Task<ReservationDto?> GetReservationByIdAsync(Guid id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        return _mapper.Map<ReservationDto>(reservation);
    }

    public async Task<ReservationDto> CreateReservationAsync(CreateReservationDto createReservationDto)
    {
        // Validar que la habitación existe
        var room = await _roomRepository.GetByIdAsync(createReservationDto.RoomId);
        if (room == null) throw new NotFoundException($"Room with ID {createReservationDto.RoomId} not found");

        //TODO: Validar que el huésped existe
        var guest = await _guestRepository.GetByIdAsync(createReservationDto.GuestId);
        if (guest == null) throw new NotFoundException($"Guest with ID {createReservationDto.GuestId} not found");

        //TODO: Validar disponibilidad de la habitación
        var availableRooms = await _roomRepository.GetAvailableRoomsAsync(
            createReservationDto.CheckInDate,
            createReservationDto.CheckOutDate);

        if (!availableRooms.Any(r => r.Id == createReservationDto.RoomId))
        {
            throw new BusinessException("The room is not available for the selected dates");
        }

        //TODO: Calcular el precio total
        var nights = (createReservationDto.CheckOutDate - createReservationDto.CheckInDate).Days;
        var totalPrice = room.PricePerNight * nights;

        var reservation = _mapper.Map<Reservation>(createReservationDto);
        reservation.TotalPrice = totalPrice;

        await _reservationRepository.AddAsync(reservation);
        await _reservationRepository.SaveChangesAsync();

        return _mapper.Map<ReservationDto>(reservation);
    }

    public async Task UpdateReservationAsync(Guid id, CreateReservationDto updateReservationDto)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        if (reservation == null) throw new NotFoundException($"Reservation with ID {id} not found");

        //TODO: Realizar las mismas validaciones que en Create
        var room = await _roomRepository.GetByIdAsync(updateReservationDto.RoomId);
        if (room == null) throw new NotFoundException($"Room with ID {updateReservationDto.RoomId} not found");

        var guest = await _guestRepository.GetByIdAsync(updateReservationDto.GuestId);
        if (guest == null) throw new NotFoundException($"Guest with ID {updateReservationDto.GuestId} not found");

        //TODO: Validar disponibilidad solo si las fechas o la habitación cambiaron
        if (reservation.RoomId != updateReservationDto.RoomId ||
            reservation.CheckInDate != updateReservationDto.CheckInDate ||
            reservation.CheckOutDate != updateReservationDto.CheckOutDate)
        {
            var availableRooms = await _roomRepository.GetAvailableRoomsAsync(
                updateReservationDto.CheckInDate,
                updateReservationDto.CheckOutDate);

            if (!availableRooms.Any(r => r.Id == updateReservationDto.RoomId))
            {
                throw new BusinessException("The room is not available for the selected dates");
            }
        }

        // Actualizar el precio total
        var nights = (updateReservationDto.CheckOutDate - updateReservationDto.CheckInDate).Days;
        var totalPrice = room.PricePerNight * nights;

        _mapper.Map(updateReservationDto, reservation);
        reservation.TotalPrice = totalPrice;

        await _reservationRepository.UpdateAsync(reservation);
        await _reservationRepository.SaveChangesAsync();
    }

    public async Task DeleteReservationAsync(Guid id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        if (reservation == null) throw new NotFoundException($"Reservation with ID {id} not found");

        await _reservationRepository.DeleteAsync(reservation);
        await _reservationRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<ReservationDto>> GetReservationsByGuestIdAsync(Guid guestId)
    {
        var reservations = await _reservationRepository.GetReservationsByGuestIdAsync(guestId);
        return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
    }

    public async Task<IEnumerable<ReservationDto>> GetActiveReservationsAsync()
    {
        var reservations = await _reservationRepository.GetActiveReservationsAsync();
        return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
    }
}
