using MediatR;

namespace PetHotel.Application.UseCases.Review.Command.DeleteReview;

public class DeleteReviewCommand(int reviewId, int hotelId) : IRequest
{
    public int ReviewId { get; set; } = reviewId;
    public int HotelId { get; set; } = hotelId;
}
