namespace HotelBooking.Application.Services
{
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

        public async Task<RoomDto> GetRoomByIdAsync(string id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
                throw new NotFoundException($"Room with ID {id} not found");

            return _mapper.Map<RoomDto>(room);
        }

        public async Task<RoomDto> CreateRoomAsync(CreateRoomDto roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            await _roomRepository.CreateAsync(room);
            return _mapper.Map<RoomDto>(room);
        }

        public async Task UpdateRoomAsync(string id, UpdateRoomDto roomDto)
        {
            var existingRoom = await _roomRepository.GetByIdAsync(id);
            if (existingRoom == null)
                throw new NotFoundException($"Room with ID {id} not found");

            var room = _mapper.Map<Room>(roomDto);
            room.Id = id;
            await _roomRepository.UpdateAsync(id, room);
        }

        public async Task DeleteRoomAsync(string id)
        {
            if (!await _roomRepository.ExistsAsync(id))
                throw new NotFoundException($"Room with ID {id} not found");

            await _roomRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut)
        {
            var rooms = await _roomRepository.GetAvailableRoomsAsync(checkIn, checkOut);
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task<bool> IsRoomAvailableAsync(string roomId, DateTime checkIn, DateTime checkOut)
        {
            return await _roomRepository.IsRoomAvailableAsync(roomId, checkIn, checkOut);
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByTypeAsync(string roomType)
        {
            var rooms = await _roomRepository.GetRoomsByTypeAsync(roomType);
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }
    }
}