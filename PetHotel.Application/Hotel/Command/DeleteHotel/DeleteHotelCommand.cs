using MediatR;

namespace PetHotel.Application.Hotel.Command.DeleteHotel;

public class DeleteHotelCommand(int id): IRequest
{
    public int Id { get; set; } = id;
}
