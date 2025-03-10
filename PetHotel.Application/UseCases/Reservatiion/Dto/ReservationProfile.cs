﻿using AutoMapper;
using PetHotel.Application.UseCases.Reservatiion.CreateReservation;

namespace PetHotel.Application.UseCases.Reservatiion.Dto;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<Domain.Entities.Reservation, ReservationDto>()
            .ForMember(s => s.Services, opt => opt.MapFrom(src => src.ReservationServices));

        CreateMap<CreateReservationCommand, Domain.Entities.Reservation>();
    }
}
