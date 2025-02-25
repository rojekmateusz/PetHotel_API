using MediatR;
using PetHotel.Application.UseCases.Owner.Dto;

namespace PetHotel.Application.UseCases.Owner.Queries.GetOwnerById;

public class GetOwnerByIDQuery(int id) : IRequest<OwnerDto>
{
    public int Id { get; set; } = id;
}
