using MediatR;

namespace PetHotel.Application.UseCases.Hotel.Command.DeleteHotel;

public class DeleteHotelCommand(int hotelId) : IRequest
{
    public int Id { get; set; } = hotelId;
}
