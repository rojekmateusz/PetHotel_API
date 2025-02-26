using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Repositories;

public interface IReviewRepository
{
    Task<int> CreateReview(Review entity);
    Task DeleteReview(Review entity);
    Task<int> UpdateReview(Review entity);
    Task SaveChanges();
}
