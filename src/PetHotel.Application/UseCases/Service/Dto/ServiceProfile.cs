using AutoMapper;
using PetHotel.Application.UseCases.Service.Command.CreateService;
using PetHotel.Application.UseCases.Service.Command.UpdateService;

namespace PetHotel.Application.UseCases.Service.Dto;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Domain.Entities.Service, ServiceDto>();
        CreateMap<CreateServiceCommand, Domain.Entities.Service>();
        CreateMap<UpdateServiceCommand, Domain.Entities.Service>();
    }
}
