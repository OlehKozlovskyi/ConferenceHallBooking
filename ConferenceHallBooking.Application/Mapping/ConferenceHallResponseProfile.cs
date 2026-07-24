using AutoMapper;
using ConferenceHallBooking.Application.DTOs.Responses;
using ConferenceHallBooking.Domain.Entitities;

namespace ConferenceHallBooking.Application.Mapping
{
    public class ConferenceHallResponseProfile : Profile
    {
        public ConferenceHallResponseProfile()
        {
            CreateMap<Hall, ConferenceHallResponse>();

            CreateMap<Amenities, AmentitiesResponse>();
        }
    }
}
