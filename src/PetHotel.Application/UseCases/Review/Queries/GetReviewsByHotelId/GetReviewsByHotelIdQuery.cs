using MediatR;
using PetHotel.Application.UseCases.Review.Dto;

namespace PetHotel.Application.UseCases.Review.Queries.GetReviewsByHotelId;

public class GetReviewsByHotelIdQuery(int HotelId): IRequest<IEnumerable<ReviewDto>>
{
    public int HotelID { get; set; } = HotelId;
}
