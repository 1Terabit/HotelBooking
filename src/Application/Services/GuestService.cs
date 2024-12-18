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

    public async Task<GuestDto?> GetGuestByIdAsync(Guid id)
    {
        var guest = await _guestRepository.GetByIdAsync(id);
        return _mapper.Map<GuestDto>(guest);
    }

    public async Task<GuestDto> CreateGuestAsync(CreateGuestDto createGuestDto)
    {
        //TODO: Verificar si ya existe un huésped con el mismo email
        var existingGuest = await _guestRepository.GetGuestByEmailAsync(createGuestDto.Email);
        if (existingGuest != null)
        {
            throw new BusinessException("A guest with this email already exists");
        }

        var guest = _mapper.Map<Guest>(createGuestDto);
        await _guestRepository.AddAsync(guest);
        await _guestRepository.SaveChangesAsync();
        return _mapper.Map<GuestDto>(guest);
    }

    public async Task UpdateGuestAsync(Guid id, CreateGuestDto updateGuestDto)
    {
        var guest = await _guestRepository.GetByIdAsync(id);
        if (guest == null) throw new NotFoundException($"Guest with ID {id} not found");

        //TODO: Verificar si el nuevo email ya está en uso por otro huésped
        var existingGuest = await _guestRepository.GetGuestByEmailAsync(updateGuestDto.Email);
        if (existingGuest != null && existingGuest.Id != id)
        {
            throw new BusinessException("A guest with this email already exists");
        }

        _mapper.Map(updateGuestDto, guest);
        await _guestRepository.UpdateAsync(guest);
        await _guestRepository.SaveChangesAsync();
    }

    public async Task DeleteGuestAsync(Guid id)
    {
        var guest = await _guestRepository.GetByIdAsync(id);
        if (guest == null) throw new NotFoundException($"Guest with ID {id} not found");

        await _guestRepository.DeleteAsync(guest);
        await _guestRepository.SaveChangesAsync();
    }

    public async Task<GuestDto?> GetGuestByEmailAsync(string email)
    {
        var guest = await _guestRepository.GetGuestByEmailAsync(email);
        return _mapper.Map<GuestDto>(guest);
    }
}