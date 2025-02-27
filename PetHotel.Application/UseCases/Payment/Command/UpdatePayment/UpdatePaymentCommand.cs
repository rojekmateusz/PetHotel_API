using MediatR;

namespace PetHotel.Application.UseCases.Payment.Command.UpdatePayment;

public class UpdatePaymentCommand : IRequest
{
    public int Id { get; set; }
    public decimal Amount { get; set; } = default!;
    public DateTime PaymentDate { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int OwnerId { get; set; }
}
