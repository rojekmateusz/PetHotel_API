using MediatR;

namespace PetHotel.Application.UseCases.Review.Command.CreateReview;

public class CreateReviewCommand : IRequest<int>
{
    public int Rating { get; set; } = default!;
    public string Comment { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int HotelId { get; set; } 
}
