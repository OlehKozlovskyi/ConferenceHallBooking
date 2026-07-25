using AutoMapper;
using ConferenceHallBooking.Application.DTOs.Responses;
using ConferenceHallBooking.Domain.Entitities;

namespace ConferenceHallBooking.Application.Mapping
{
    public class ConferenceHallResponseProfile : Profile
    {
        public ConferenceHallResponseProfile()
        {
            CreateMap<Hall, ConferenceHallResponse>()
                .ForMember(dest => dest.BasePricePerHour, h => h.MapFrom(src => src.PricePerHour));

            CreateMap<Amenities, AmentitiesResponse>();
        }
    }
}
