public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        
        CreateMap<Hotel, HotelDto>().ReverseMap();
        CreateMap<CreateHotelDto, Hotel>();
        CreateMap<UpdateHotelDto, Hotel>();

        
        CreateMap<RoomEntity, RoomDto>().ReverseMap();
        CreateMap<CreateRoomDto, Room>();
        CreateMap<UpdateRoomDto, Room>();

        
        CreateMap<Guest, GuestDto>().ReverseMap();
        CreateMap<CreateGuestDto, Guest>();
        CreateMap<EmergencyContact, EmergencyContactDto>().ReverseMap();

       
        CreateMap<Reservation, ReservationDto>()
            .ForMember(dest => dest.Guest, opt => opt.Ignore())
            .ForMember(dest => dest.Room, opt => opt.Ignore());
        CreateMap<CreateReservationDto, Reservation>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending"))
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore());
        CreateMap<UpdateReservationDto, Reservation>();
    }
}
