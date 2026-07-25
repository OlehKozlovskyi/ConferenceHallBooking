using AutoMapper;
using ConferenceHallBooking.Application.DTOs.Requests;
using ConferenceHallBooking.Domain.Entitities;

namespace ConferenceHallBooking.Application.Mapping
{
    public class CreateBookingProfile : Profile
    {
        public CreateBookingProfile()
        {
            CreateMap<CreateBookingRequest, Booking>()
                .ForMember(dest => dest.Id, b => b.Ignore())
                .ForMember(dest => dest.HallId, b => b.MapFrom(src => src.HallId))
                .ForMember(dest => dest.StartTime, b => b.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndTime, b => b.MapFrom(src => src.StartDate + src.DurationsInHours))
                .ForMember(dest => dest.TotalPrice, b => b.Ignore())
                .ForMember(dest => dest.Hall, b => b.Ignore());
        }
    }
}
