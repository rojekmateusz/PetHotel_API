using MediatR;

namespace PetHotel.Application.UseCases.Animal.Command.DeleteAnimal;

public class DeleteAnimalCommand(int ownerId, int id) : IRequest
{
    public int OwnerId { get; set; } = ownerId;
    public int Id { get; set; } = id;
}
