using MediatR;

namespace PetHotel.Application.UseCases.Hotel.Command.DeleteHotel;

public class DeleteAnimalCommand(int hotelId) : IRequest
{
    public int Id { get; set; } = hotelId;
}
