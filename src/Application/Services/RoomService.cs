public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper _mapper;

    public RoomService(IRoomRepository roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync()
    {
        var rooms = await _roomRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<RoomDto>>(rooms);
    }

    public async Task<RoomDto?> GetRoomByIdAsync(Guid id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        return _mapper.Map<RoomDto>(room);
    }

    public async Task<RoomDto> CreateRoomAsync(CreateRoomDto createRoomDto)
    {
        var room = _mapper.Map<Room>(createRoomDto);
        await _roomRepository.AddAsync(room);
        await _roomRepository.SaveChangesAsync();
        return _mapper.Map<RoomDto>(room);
    }

    public async Task UpdateRoomAsync(Guid id, CreateRoomDto updateRoomDto)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null) throw new NotFoundException($"Room with ID {id} not found");

        _mapper.Map(updateRoomDto, room);
        await _roomRepository.UpdateAsync(room);
        await _roomRepository.SaveChangesAsync();
    }

    public async Task DeleteRoomAsync(Guid id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null) throw new NotFoundException($"Room with ID {id} not found");

        await _roomRepository.DeleteAsync(room);
        await _roomRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut)
    {
        var rooms = await _roomRepository.GetAvailableRoomsAsync(checkIn, checkOut);
        return _mapper.Map<IEnumerable<RoomDto>>(rooms);
    }

    public async Task<IEnumerable<RoomDto>> GetRoomsByHotelIdAsync(Guid hotelId)
    {
        var rooms = await _roomRepository.GetRoomsByHotelIdAsync(hotelId);
        return _mapper.Map<IEnumerable<RoomDto>>(rooms);
    }
}