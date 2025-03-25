using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Review.Dto;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Review.Queries.GetReviewByIdForHotel;

public class GetReviewByIdForHotelQueryHandler(ILogger<GetReviewByIdForHotelQueryHandler> logger, IMapper mapper, IHotelRepository repository) : IRequestHandler<GetReviewByIdForHotelQuery, ReviewDto>
{
    public async Task<ReviewDto> Handle(GetReviewByIdForHotelQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting review {ReviewId} for hotel {HotelId}", request.ReviewId, request.HotelId);
        var hotel = await repository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        var review = hotel.Reviews.FirstOrDefault(r => r.Id == request.ReviewId)
            ?? throw new NotFoundException(nameof(Review), request.ReviewId.ToString());

        var reviewDto = mapper.Map<ReviewDto>(review);
        return reviewDto;
    }
}
