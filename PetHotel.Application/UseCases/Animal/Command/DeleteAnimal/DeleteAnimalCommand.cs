using MediatR;

namespace PetHotel.Application.UseCases.Animal.Command.DeleteAnimal;

public class DeleteAnimalCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
}
