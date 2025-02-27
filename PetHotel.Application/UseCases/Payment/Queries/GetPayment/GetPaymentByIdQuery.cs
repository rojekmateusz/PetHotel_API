using MediatR;
using PetHotel.Application.UseCases.Payment.Dto;

namespace PetHotel.Application.UseCases.Payment.Queries.GetPayment;

public class GetPaymentByIdQuery(int ownerId, int paymentId): IRequest<PaymentDto>
{
    public int OwnerId { get; set; } = ownerId;
    public int PaymentId { get; set; } = paymentId;
}
