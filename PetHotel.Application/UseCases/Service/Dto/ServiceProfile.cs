﻿using AutoMapper;

namespace PetHotel.Application.UseCases.Service.Dto;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Domain.Entities.Service, ServiceDto>()
            .ForMember(r => r.Reservations, opt => opt.MapFrom(src => src.ReservationServices));
    }
}
