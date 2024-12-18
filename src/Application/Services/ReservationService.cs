namespace HotelBooking.Application.Services
{
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

        public async Task<ReservationDto> GetReservationByIdAsync(string id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
                throw new NotFoundException($"Reservation with ID {id} not found");

            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<ReservationDto> CreateReservationAsync(CreateReservationDto reservationDto)
        {
            if (!await _guestRepository.ExistsAsync(reservationDto.GuestId))
                throw new NotFoundException($"Guest with ID {reservationDto.GuestId} not found");

            if (!await _roomRepository.ExistsAsync(reservationDto.RoomId))
                throw new NotFoundException($"Room with ID {reservationDto.RoomId} not found");

            if (!await _roomRepository.IsRoomAvailableAsync(reservationDto.RoomId, reservationDto.CheckInDate, reservationDto.CheckOutDate))
                throw new BusinessException("Room is not available for the selected dates");

            if (reservationDto.CheckInDate >= reservationDto.CheckOutDate)
                throw new BusinessException("Check-out date must be after check-in date");

            if (reservationDto.CheckInDate < DateTime.Today)
                throw new BusinessException("Check-in date cannot be in the past");

            var totalPrice = await CalculateTotalPriceAsync(reservationDto.RoomId, reservationDto.CheckInDate, reservationDto.CheckOutDate);

            var reservation = _mapper.Map<Reservation>(reservationDto);
            reservation.TotalPrice = totalPrice;
            reservation.Status = ReservationStatus.Active;
            reservation.CreatedAt = DateTime.UtcNow;

            await _reservationRepository.CreateAsync(reservation);
            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task UpdateReservationAsync(string id, UpdateReservationDto reservationDto)
        {
            var existingReservation = await _reservationRepository.GetByIdAsync(id);
            if (existingReservation == null)
                throw new NotFoundException($"Reservation with ID {id} not found");

            if (reservationDto.RoomId != existingReservation.RoomId ||
                reservationDto.CheckInDate != existingReservation.CheckInDate ||
                reservationDto.CheckOutDate != existingReservation.CheckOutDate)
            {
                if (!await _roomRepository.IsRoomAvailableAsync(
                    reservationDto.RoomId ?? existingReservation.RoomId,
                    reservationDto.CheckInDate ?? existingReservation.CheckInDate,
                    reservationDto.CheckOutDate ?? existingReservation.CheckOutDate))
                {
                    throw new BusinessException("Room is not available for the selected dates");
                }
            }

            var reservation = _mapper.Map<Reservation>(reservationDto);
            reservation.Id = id;
            reservation.UpdatedAt = DateTime.UtcNow;

            await _reservationRepository.UpdateAsync(id, reservation);
        }

        public async Task DeleteReservationAsync(string id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
                throw new NotFoundException($"Reservation with ID {id} not found");

            reservation.Status = ReservationStatus.Cancelled;
            reservation.UpdatedAt = DateTime.UtcNow;
            await _reservationRepository.UpdateAsync(id, reservation);
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                throw new BusinessException("Start date must be before end date");

            var reservations = await _reservationRepository.GetReservationsByDateRangeAsync(startDate, endDate);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<IEnumerable<ReservationDto>> GetActiveReservationsAsync()
        {
            var reservations = await _reservationRepository.GetActiveReservationsAsync();
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<decimal> CalculateTotalPriceAsync(string roomId, DateTime checkIn, DateTime checkOut)
        {
            return await _reservationRepository.CalculateTotalPriceAsync(roomId, checkIn, checkOut);
        }
    }
}