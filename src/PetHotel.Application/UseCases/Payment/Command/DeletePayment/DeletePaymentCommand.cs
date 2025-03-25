using MediatR;
using System.Net.NetworkInformation;

namespace PetHotel.Application.UseCases.Payment.Command.DeletePayment;

public class DeletePaymentCommand(int ownerId, int paymentId): IRequest
{
    public int OwnerId { get; set; } = ownerId;
    public int PaymentId { get; set; } = paymentId;
}

