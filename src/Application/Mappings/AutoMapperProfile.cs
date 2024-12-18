public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Hotel, HotelDto>();
        CreateMap<CreateHotelDto, Hotel>();

        CreateMap<Room, RoomDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.RoomType.Name));
        CreateMap<CreateRoomDto, Room>();

        CreateMap<Guest, GuestDto>();
        CreateMap<CreateGuestDto, Guest>();

        CreateMap<Reservation, ReservationDto>();
        CreateMap<CreateReservationDto, Reservation>();
    }
}
