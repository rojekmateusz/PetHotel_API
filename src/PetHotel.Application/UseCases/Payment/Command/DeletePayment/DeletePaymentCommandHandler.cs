using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Payment.Command.DeletePayment;

public class DeletePaymentCommandHandler(ILogger<DeletePaymentCommandHandler> logger, IOwnerRepository ownerRepository, IPaymentRepository paymentRepository) :
    IRequestHandler<DeletePaymentCommand>
{
    public async Task Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting payment with Id: {@PaymentID} in Owner {@OwnerID}", request.PaymentId, request.OwnerId);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.OwnerId)
            ?? throw new NotFoundException(nameof(Owner), request.OwnerId.ToString());
        var payment = owner.Payments.FirstOrDefault(p => p.Id == request.PaymentId)
            ?? throw new NotFoundException(nameof(Payment), request.PaymentId.ToString());

        await paymentRepository.DeletePayment(payment);
    }
}
