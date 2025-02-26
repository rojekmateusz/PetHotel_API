using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Review.Dto;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Review.Queries.GetReviewsByHotelId;

public class GetReviewsByHotelIdQueryHandler(ILogger<GetReviewsByHotelIdQueryHandler> logger, IMapper mapper, IHotelRepository hotelRepository) : IRequestHandler<GetReviewsByHotelIdQuery, IEnumerable<ReviewDto>>
{
    public async Task<IEnumerable<ReviewDto>> Handle(GetReviewsByHotelIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting reviews for hotel {HotelId}", request.HotelID);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelID)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelID.ToString());
        var results = mapper.Map<IEnumerable<ReviewDto>>(hotel.Reviews);
        return results;
    }
}
