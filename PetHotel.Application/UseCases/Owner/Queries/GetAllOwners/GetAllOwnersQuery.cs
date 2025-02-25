using MediatR;
using PetHotel.Application.UseCases.Owner.Dto;

namespace PetHotel.Application.UseCases.Owner.Queries.GetAllOwners;

public class GetAllOwnersQuery : IRequest<IEnumerable<OwnerDto>>
{
}
