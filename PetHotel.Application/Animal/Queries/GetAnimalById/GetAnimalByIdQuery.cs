using MediatR;
using PetHotel.Application.Animal.Dto;

namespace PetHotel.Application.Animal.Queries.GetAnimalById;

public class GetAnimalByIdQuery(int id): IRequest<AnimalDto>
{
    public int Id { get; set; } = id;
}
