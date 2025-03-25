using MediatR;

namespace PetHotel.Application.UseCases.Service.Command.DeleteService;

public class DeleteServiceCommand(int hotelId, int serviceId) : IRequest
{
    public int HotelId { get; set; } = hotelId;
    public int ServiceId { get; set; } = serviceId;
}
