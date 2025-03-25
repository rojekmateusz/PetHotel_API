using MediatR;

namespace PetHotel.Application.UseCases.Payment.Command.CreatePayment;

public class CreatePaymentCommand: IRequest<int>
{
    public decimal Amount { get; set; } = default!;
    public DateTime PaymentDate { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public int OwnerId { get; set; }
}
