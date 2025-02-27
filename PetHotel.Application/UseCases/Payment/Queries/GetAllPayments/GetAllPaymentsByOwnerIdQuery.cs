using MediatR;
using PetHotel.Application.UseCases.Payment.Dto;

namespace PetHotel.Application.UseCases.Payment.Queries.GetAllPayments;

public class GetAllPaymentsByOwnerIdQuery(int ownerId): IRequest<IEnumerable<PaymentDto>>
{
    public int OwnerId { get; set; } = ownerId;
}
