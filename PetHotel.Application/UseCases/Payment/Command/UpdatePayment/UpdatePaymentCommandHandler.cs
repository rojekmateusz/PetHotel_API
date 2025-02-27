using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Payment.Dto;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Payment.Command.UpdatePayment;

public class UpdatePaymentCommandHandler(ILogger<UpdatePaymentCommandHandler> logger, IMapper mapper, IOwnerRepository ownerRepository, IPaymentRepository paymentRepository) :
    IRequestHandler<UpdatePaymentCommand>
{
    public async Task Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updated payment with Id: {@PaymentID} in Owner: {@OwnerID}", request.Id, request.OwnerId);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.OwnerId)
            ?? throw new NotFoundException(nameof(Owner), request.Id.ToString());
        var payment = owner.Payments.FirstOrDefault(p => p.Id == request.Id);
        mapper.Map(request, payment);
        await paymentRepository.SaveChanges();
    }
}
