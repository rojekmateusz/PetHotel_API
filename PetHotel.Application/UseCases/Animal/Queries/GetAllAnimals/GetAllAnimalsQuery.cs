using MediatR;
using PetHotel.Application.UseCases.Animal.Dto;

namespace PetHotel.Application.UseCases.Animal.Queries.GetAllAnimals;

public class GetAllAnimalsQuery : IRequest<IEnumerable<AnimalDto>>
{
}
