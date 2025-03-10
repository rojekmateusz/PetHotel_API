using MediatR;
using PetHotel.Application.UseCases.Service.Dto;

namespace PetHotel.Application.UseCases.Service.Queries.GetServiceByID;

public class GetServiceByIdQuery(int hotelId, int serviceId) : IRequest<ServiceDto>
{
    public int Id { get; set; } = serviceId;
    public int HotelId { get; set; } = hotelId;
}
