namespace HotelBooking.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelService(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HotelDto>> GetAllHotelsAsync()
        {
            var hotels = await _hotelRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<HotelDto>>(hotels);
        }

        public async Task<HotelDto> GetHotelByIdAsync(string id)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id);
            if (hotel == null)
                throw new NotFoundException($"Hotel with ID {id} not found");

            return _mapper.Map<HotelDto>(hotel);
        }

        public async Task<HotelDto> CreateHotelAsync(CreateHotelDto hotelDto)
        {
            var hotel = _mapper.Map<Hotel>(hotelDto);
            await _hotelRepository.CreateAsync(hotel);
            return _mapper.Map<HotelDto>(hotel);
        }

        public async Task UpdateHotelAsync(string id, UpdateHotelDto hotelDto)
        {
            var existingHotel = await _hotelRepository.GetByIdAsync(id);
            if (existingHotel == null)
                throw new NotFoundException($"Hotel with ID {id} not found");

            var hotel = _mapper.Map<Hotel>(hotelDto);
            hotel.Id = id;
            await _hotelRepository.UpdateAsync(id, hotel);
        }

        public async Task DeleteHotelAsync(string id)
        {
            if (!await _hotelRepository.ExistsAsync(id))
                throw new NotFoundException($"Hotel with ID {id} not found");

            await _hotelRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<HotelDto>> GetHotelsByRatingAsync(int rating)
        {
            var hotels = await _hotelRepository.GetHotelsByRatingAsync(rating);
            return _mapper.Map<IEnumerable<HotelDto>>(hotels);
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByHotelIdAsync(string hotelId)
        {
            var rooms = await _hotelRepository.GetRoomsByHotelIdAsync(hotelId);
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }
    }
}