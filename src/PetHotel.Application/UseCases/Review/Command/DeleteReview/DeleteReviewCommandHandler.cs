using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Review.Command.DeleteReview;

public class DeleteReviewCommandHandler(ILogger<DeleteReviewCommandHandler> logger, IHotelRepository hotelRepository, IReviewRepository reviewRepository) : IRequestHandler<DeleteReviewCommand>
{
    public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting review {ReviewId} for hotel {HotelId}", request.ReviewId, request.HotelId);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        var result = hotel.Reviews.FirstOrDefault(r => r.Id == request.ReviewId)
            ?? throw new NotFoundException(nameof(Review), request.ReviewId.ToString());
        
       await reviewRepository.DeleteReview(result);
    }
}
