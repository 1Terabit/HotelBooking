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
        var hotels = await _hotelRepository.GetHotelsWithRoomsAsync();
        return _mapper.Map<IEnumerable<HotelDto>>(hotels);
    }

    public async Task<HotelDto?> GetHotelByIdAsync(Guid id)
    {
        var hotel = await _hotelRepository.GetHotelWithRoomsAsync(id);
        return _mapper.Map<HotelDto>(hotel);
    }

    public async Task<HotelDto> CreateHotelAsync(CreateHotelDto createHotelDto)
    {
        var hotel = _mapper.Map<Hotel>(createHotelDto);
        await _hotelRepository.AddAsync(hotel);
        await _hotelRepository.SaveChangesAsync();
        return _mapper.Map<HotelDto>(hotel);
    }

    public async Task UpdateHotelAsync(Guid id, CreateHotelDto updateHotelDto)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id);
        if (hotel == null) throw new NotFoundException($"Hotel with ID {id} not found");

        _mapper.Map(updateHotelDto, hotel);
        await _hotelRepository.UpdateAsync(hotel);
        await _hotelRepository.SaveChangesAsync();
    }

    public async Task DeleteHotelAsync(Guid id)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id);
        if (hotel == null) throw new NotFoundException($"Hotel with ID {id} not found");

        await _hotelRepository.DeleteAsync(hotel);
        await _hotelRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<HotelDto>> GetHotelsByLocationAsync(string city, string country)
    {
        var hotels = await _hotelRepository.GetHotelsByLocationAsync(city, country);
        return _mapper.Map<IEnumerable<HotelDto>>(hotels);
    }
}
