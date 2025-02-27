using AutoMapper;
using PetHotel.Application.UseCases.Payment.Command.CreatePayment;

namespace PetHotel.Application.UseCases.Payment.Dto;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<Domain.Entities.Payment, PaymentDto>();
        CreateMap<CreatePaymentCommand, Domain.Entities.Payment>();
    }
}
