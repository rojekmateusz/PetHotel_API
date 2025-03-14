using MediatR;
using PetHotel.Application.UseCases.Animal.Dto;

namespace PetHotel.Application.UseCases.Animal.Queries.GetAllAnimals;

public class GetAllAnimalsQuery(int ownerId) : IRequest<IEnumerable<AnimalDto>>
{
    public int OwnerId { get; set; } = ownerId;
}
