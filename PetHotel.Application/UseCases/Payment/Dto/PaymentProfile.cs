using AutoMapper;

namespace PetHotel.Application.UseCases.Payment.Dto;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<Domain.Entities.Payment, PaymentDto>();
    }
}
