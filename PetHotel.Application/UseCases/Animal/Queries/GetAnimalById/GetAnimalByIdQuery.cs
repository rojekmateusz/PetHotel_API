using MediatR;
using PetHotel.Application.UseCases.Animal.Dto;

namespace PetHotel.Application.UseCases.Animal.Queries.GetAnimalById;

public class GetAnimalByIdQuery(int id) : IRequest<AnimalDto>
{
    public int Id { get; set; } = id;
}
