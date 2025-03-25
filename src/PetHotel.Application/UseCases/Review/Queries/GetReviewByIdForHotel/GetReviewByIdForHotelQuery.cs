using MediatR;
using PetHotel.Application.UseCases.Review.Dto;

namespace PetHotel.Application.UseCases.Review.Queries.GetReviewByIdForHotel;

public class GetReviewByIdForHotelQuery(int reviewId, int hotelId) : IRequest<ReviewDto>
{
    public int HotelId { get; set; } = hotelId;
    public int ReviewId { get; set; } = reviewId;
}
