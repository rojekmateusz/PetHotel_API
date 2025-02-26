using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Repositories;

public interface IReviewRepository
{
    Task<int> CreateReview(Review entity);
}
