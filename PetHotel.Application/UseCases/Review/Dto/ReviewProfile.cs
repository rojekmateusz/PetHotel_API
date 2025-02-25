using AutoMapper;

namespace PetHotel.Application.UseCases.Review.Dto;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Domain.Entities.Review, ReviewDto>();
    }
}
