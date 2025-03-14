using MediatR;
using PetHotel.Application.UseCases.Animal.Dto;

namespace PetHotel.Application.UseCases.Animal.Queries.GetAnimalById;

public class GetAnimalByIdQuery(int ownerId, int id) : IRequest<AnimalDto>
{
    public int OwnerId { get; set; } = ownerId; 
    public int Id { get; set; } = id;
}
