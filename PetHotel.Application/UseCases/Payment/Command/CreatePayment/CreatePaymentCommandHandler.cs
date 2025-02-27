using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Payment.Command.CreatePayment;

public class CreatePaymentCommandHandler(ILogger<CreatePaymentCommandHandler> logger, IMapper mapper, IOwnerRepository ownerRepository, IPaymentRepository paymentRepository) :
    IRequestHandler<CreatePaymentCommand, int>
{
    public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Created payment for Owner with Id: {@OwnerId}", request.OwnerId);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.OwnerId)
            ?? throw new NotFoundException(nameof(Owner), request.OwnerId.ToString());
        var payment = mapper.Map<Domain.Entities.Payment>(request);
        await paymentRepository.CreatePayment(payment);
        return payment.Id;
    }
}
