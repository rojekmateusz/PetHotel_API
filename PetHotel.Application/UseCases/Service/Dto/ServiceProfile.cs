using AutoMapper;
using PetHotel.Application.UseCases.Service.Command.CreateService;

namespace PetHotel.Application.UseCases.Service.Dto;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Domain.Entities.Service, ServiceDto>();
   //         .ForMember(r => r.Reservations, opt => opt.MapFrom(src => src.ReservationServices));
        CreateMap<CreateServiceCommand, Domain.Entities.Service>();


    }
}
