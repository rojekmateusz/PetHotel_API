using MediatR;
using PetHotel.Application.Animal.Dto;

namespace PetHotel.Application.Animal.Queries.GetAllAnimal;

public class GetAllAnimalsQuery: IRequest<IEnumerable<AnimalDto>>
{
}
