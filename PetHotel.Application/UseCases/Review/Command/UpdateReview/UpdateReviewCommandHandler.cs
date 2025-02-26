using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Review.Dto;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Review.Command.UpdateReview;

public class UpdateReviewCommandHandler(ILogger<UpdateReviewCommandHandler> logger, IMapper mapper, IHotelRepository hotelRepository, IReviewRepository reviewRepository) :
    IRequestHandler<UpdateReviewCommand>
{
    public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating review with Id: {ReviewID} for Hotel {HotelId}", request.Id, request.HotelId);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());
        var review = hotel.Reviews.FirstOrDefault(r => r.Id == request.Id)
            ?? throw new NotFoundException(nameof(Review), request.Id.ToString());
        mapper.Map(request, review);
        await reviewRepository.SaveChanges();
    }
}
