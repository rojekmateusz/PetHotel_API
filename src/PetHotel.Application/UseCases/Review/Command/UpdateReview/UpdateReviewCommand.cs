using MediatR;

namespace PetHotel.Application.UseCases.Review.Command.UpdateReview
{
    public class UpdateReviewCommand: IRequest
    {
        public int Id { get; set; }
        public int Rating { get; set; } = default!;
        public string Comment { get; set; } = default!;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int HotelId { get; set; }
    }
}
