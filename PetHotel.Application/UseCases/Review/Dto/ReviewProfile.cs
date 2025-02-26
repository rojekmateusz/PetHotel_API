using AutoMapper;
using PetHotel.Application.UseCases.Review.Command.CreateReview;
using PetHotel.Application.UseCases.Review.Command.UpdateReview;

namespace PetHotel.Application.UseCases.Review.Dto;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Domain.Entities.Review, ReviewDto>();
        CreateMap<CreateReviewCommand, Domain.Entities.Review>();
        CreateMap<UpdateReviewCommand, Domain.Entities.Review>();
    }
}
