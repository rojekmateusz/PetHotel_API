using AutoMapper;
using PetHotel.Application.UseCases.Reservation.Command.CreateReservation;
using PetHotel.Application.UseCases.Service.Dto;
using PetHotel.Domain.Entities;

namespace PetHotel.Application.UseCases.Reservatiion.Dto;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<Domain.Entities.Reservation, ReservationDto>()
            .ForMember(s => s.ReservationServices, opt => opt.MapFrom(src => src.ReservationServices.Select(rs => new ReservationServicesDto
            {
                ReservationId = rs.ReservationId,
                ServiceId = rs.ServiceId,
                Name = rs.Service.Name,
                Description = rs.Service.Description,
                Price = rs.Service.Price
            })));

        CreateMap<ReservationServicesDto, ReservationService>();
        //  .ForMember(s => s.Services, opt => opt.MapFrom(src => src.ReservationServices.Select(rs => rs.Service)));
        CreateMap<CreateReservationCommand, Domain.Entities.Reservation>();
    }
}
