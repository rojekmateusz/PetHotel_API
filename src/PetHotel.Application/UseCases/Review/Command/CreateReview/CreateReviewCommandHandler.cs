using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Review.Command.CreateReview;

public class CreateReviewCommandHandler(ILogger<CreateReviewCommandHandler> logger, IMapper mapper, IReviewRepository reviewRepository, IHotelRepository hotelRepository) :
    IRequestHandler<CreateReviewCommand, int>
{
    public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating review for hotel {HotelId}", request.HotelId);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        var review = mapper.Map<Domain.Entities.Review>(request);
        return await reviewRepository.CreateReview(review);
    }
}
