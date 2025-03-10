using MediatR;
using PetHotel.Application.UseCases.Service.Dto;

namespace PetHotel.Application.UseCases.Service.Queries.GetAllServices;

public class GetAllServicesQuery(int hotelId) : IRequest<IEnumerable<ServiceDto>>
{
    public int HotelId { get; set; } = hotelId;
}
