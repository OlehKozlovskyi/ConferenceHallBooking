using AutoMapper;
using ConferenceHallBooking.Application.DTOs.Requests;
using ConferenceHallBooking.Domain.Entitities;

namespace ConferenceHallBooking.Application.Mapping
{
    public class CreateConferenceHallProfile : Profile
    {
        public CreateConferenceHallProfile()
        {
            CreateMap<CreateConferenceHallRequest, Hall>()
                .ForMember(dest => dest.Id, h => h.Ignore())
                .ForMember(dest => dest.Name, h => h.MapFrom(src => src.Name))
                .ForMember(dest => dest.Capacity, h => h.MapFrom(src => src.Capacity))
                .ForMember(dest => dest.PricePerHour, h => h.MapFrom(src => src.BasePricePerHour))
                .ForMember(dest => dest.Amenities, h => h.MapFrom(src => src.Amenities))
                .ForMember(dest => dest.Bookings, h => h.Ignore());

            CreateMap<AmenitiesRequest, Amenities>()
                .ForMember(dest => dest.Id, a => a.Ignore())
                .ForMember(dest => dest.Name, a => a.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, a => a.MapFrom(src => src.Price))
                .ForMember(dest => dest.HallId, a => a.Ignore())
                .ForMember(dest => dest.Hall, a => a.Ignore());
        }
    }
}
