using AutoMapper;

namespace PetHotel.Application.Payment.Dto;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<Domain.Entities.Payment, PaymentDto>();
    }
}
