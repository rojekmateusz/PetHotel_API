using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Payment.Dto;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Payment.Queries.GetAllPayments;

public class GetAllPaymentsByOwnerIdQueryHandler(ILogger<GetAllPaymentsByOwnerIdQueryHandler> logger, IMapper mapper, IOwnerRepository ownerRepository,
    IOwnerAuthorizationService ownerAuthorizationService) :
    IRequestHandler<GetAllPaymentsByOwnerIdQuery, IEnumerable<PaymentDto>>
{
    public async Task<IEnumerable<PaymentDto>> Handle(GetAllPaymentsByOwnerIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all payments by Owner Id: {@OwnerId}", request.OwnerId);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.OwnerId)
            ?? throw new NotFoundException(nameof(Owner), request.OwnerId.ToString());

        if(!ownerAuthorizationService.Authorize(owner, ResourceOperation.Read))
            throw new ForbidException();

        var payments = mapper.Map<IEnumerable<PaymentDto>>(owner.Payments);
        return payments;
    }
}
