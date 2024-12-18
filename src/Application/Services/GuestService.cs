namespace HotelBooking.Application.Services
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IMapper _mapper;

        public GuestService(IGuestRepository guestRepository, IMapper mapper)
        {
            _guestRepository = guestRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GuestDto>> GetAllGuestsAsync()
        {
            var guests = await _guestRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GuestDto>>(guests);
        }

        public async Task<GuestDto> GetGuestByIdAsync(string id)
        {
            var guest = await _guestRepository.GetByIdAsync(id);
            if (guest == null)
                throw new NotFoundException($"Guest with ID {id} not found");

            return _mapper.Map<GuestDto>(guest);
        }

        public async Task<GuestDto> CreateGuestAsync(CreateGuestDto guestDto)
        {
            var existingGuest = await _guestRepository.GetGuestByEmailAsync(guestDto.Email);
            if (existingGuest != null)
                throw new BusinessException($"A guest with email {guestDto.Email} already exists");

            var guest = _mapper.Map<Guest>(guestDto);
            await _guestRepository.CreateAsync(guest);
            return _mapper.Map<GuestDto>(guest);
        }

        public async Task UpdateGuestAsync(string id, UpdateGuestDto guestDto)
        {
            var existingGuest = await _guestRepository.GetByIdAsync(id);
            if (existingGuest == null)
                throw new NotFoundException($"Guest with ID {id} not found");

            
            if (!string.IsNullOrEmpty(guestDto.Email) && guestDto.Email != existingGuest.Email)
            {
                var guestWithEmail = await _guestRepository.GetGuestByEmailAsync(guestDto.Email);
                if (guestWithEmail != null)
                    throw new BusinessException($"Email {guestDto.Email} is already in use");
            }

            var guest = _mapper.Map<Guest>(guestDto);
            guest.Id = id;
            await _guestRepository.UpdateAsync(id, guest);
        }

        public async Task DeleteGuestAsync(string id)
        {
            if (!await _guestRepository.ExistsAsync(id))
                throw new NotFoundException($"Guest with ID {id} not found");

    
            var reservations = await _guestRepository.GetGuestReservationsAsync(id);
            if (reservations.Any(r => r.Status == ReservationStatus.Active))
                throw new BusinessException("Cannot delete guest with active reservations");

            await _guestRepository.DeleteAsync(id);
        }

        public async Task<GuestDto> GetGuestByEmailAsync(string email)
        {
            var guest = await _guestRepository.GetGuestByEmailAsync(email);
            return _mapper.Map<GuestDto>(guest);
        }

        public async Task<IEnumerable<ReservationDto>> GetGuestReservationsAsync(string guestId)
        {
            if (!await _guestRepository.ExistsAsync(guestId))
                throw new NotFoundException($"Guest with ID {guestId} not found");

            var reservations = await _guestRepository.GetGuestReservationsAsync(guestId);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }
    }
}