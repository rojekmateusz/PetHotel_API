using MediatR;

namespace PetHotel.Application.UseCases.Hotel.Command.DeleteHotel;

public class DeleteAnimalCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
}
