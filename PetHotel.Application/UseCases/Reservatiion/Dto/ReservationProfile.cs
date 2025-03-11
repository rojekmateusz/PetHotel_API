using AutoMapper;
using PetHotel.Application.UseCases.Reservatiion.CreateReservation;
using PetHotel.Application.UseCases.Service.Dto;

namespace PetHotel.Application.UseCases.Reservatiion.Dto;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<Domain.Entities.Reservation, ReservationDto>()
            .ForMember(s => s.ReservationServices, opt => opt.MapFrom(src => src.ReservationServices.Select(rs => rs.ServiceId)));

      //  CreateMap<Domain.Entities.Service, ServiceDto>();

        CreateMap<CreateReservationCommand, Domain.Entities.Reservation>();
    }
}
