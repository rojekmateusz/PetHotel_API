using MediatR;
using PetHotel.Application.Owner.Dto;

namespace PetHotel.Application.Owner.Queries.GetAllOwners;

public class GetAllOwnersQuery: IRequest<IEnumerable<OwnerDto>>
{
}
