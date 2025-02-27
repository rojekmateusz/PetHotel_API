using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Payment.Dto;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Payment.Queries.GetAllPayments;

public class GetAllPaymentsByOwnerIdQueryHandler(ILogger<GetAllPaymentsByOwnerIdQueryHandler> logger, IMapper mapper, IOwnerRepository ownerRepository) :
    IRequestHandler<GetAllPaymentsByOwnerIdQuery, IEnumerable<PaymentDto>>
{
    public async Task<IEnumerable<PaymentDto>> Handle(GetAllPaymentsByOwnerIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all payments by Owner Id: {@OwnerId}", request.OwnerId);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.OwnerId)
            ?? throw new NotFoundException(nameof(Owner), request.OwnerId.ToString());
        var payments = mapper.Map<IEnumerable<PaymentDto>>(owner.Payments);
        return payments;
    }
}
