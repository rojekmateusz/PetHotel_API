using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Payment.Dto;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Payment.Queries.GetPayment;

public class GetPaymentByIdQueryHandler(ILogger<GetPaymentByIdQueryHandler> logger, IMapper mapper, IOwnerRepository ownerRepository,
    IOwnerAuthorizationService ownerAuthorizationService) : IRequestHandler<GetPaymentByIdQuery, PaymentDto>
{
    public async Task<PaymentDto> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting payment by Id: {@paymentId} with Owner: {@OwnerId}", request.PaymentId, request.OwnerId);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.OwnerId)
            ?? throw new NotFoundException(nameof(Owner), request.OwnerId.ToString());

       if(!ownerAuthorizationService.Authorize(owner, ResourceOperation.Read))
            throw new ForbidException();

        var payment = owner.Payments.FirstOrDefault(p => p.Id == request.PaymentId)
            ?? throw new NotFoundException(nameof(Payment), request.PaymentId.ToString());
        var result = mapper.Map<PaymentDto>(payment);

        return result;
    }
}
