using MediatR;
using PetHotel.Application.Owner.Dto;

namespace PetHotel.Application.Owner.Queries.GetOwnerById;

public class GetOwnerByIDQuery(int id): IRequest<OwnerDto>
{
    public int Id { get; set; } = id;
}
