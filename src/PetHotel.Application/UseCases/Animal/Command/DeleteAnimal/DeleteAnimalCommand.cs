using MediatR;

namespace PetHotel.Application.UseCases.Animal.Command.DeleteAnimal;

public class DeleteAnimalCommand(int ownerId, int animalId) : IRequest
{
    public int OwnerId { get; set; } = ownerId;
    public int Id { get; set; } = animalId;
}
